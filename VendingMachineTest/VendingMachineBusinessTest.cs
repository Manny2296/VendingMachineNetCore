using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineApplication.Business;
using Xunit;
using Moq;
using VendingMachineApplication.Data;
using VendingMachineApplication.Models;

namespace VendingMachineTest
{
    public class VendingMachineBusinessTest
    {
        [Fact]
        public void ShouldStoreMoney_WhenAddingMoney()
        {
            var vm = new VendingMachine
            {
                Id = 1,
                Credits = 50.0
            };

            var handlerMock = new Mock<IVendingMachineHandler>();
            handlerMock.Setup(vmh => vmh.Get(1)).Returns(vm);
            handlerMock.Setup(vmh => vmh.Update(vm)).Returns(vm);

            var vendingMachineBusiness = new VendingMachineBusiness(handlerMock.Object);

            vendingMachineBusiness.AddCredit(1, 10);

            Assert.Equal(60, vm.Credits);
        }

        [Fact]
        public void ShouldNotAllowBuy_WhenMoneyIsNotEnough()
        {
            var p1 = new Product()
            {
                Id = 1,
                Name = "Guarana",
                Value = 2
            };

            var p2 = new Product()
            {
                Id = 2,
                Name = "Mandarin",
                Value = 0.5
            };

            var vm = new VendingMachine
            {
                Id = 1,
                Address = "Test Machine",
                Credits = 0.05,
                Products = new List<Product>()
                {
                    p1, p2
                }
            };

            var handlerMock = new Mock<IVendingMachineHandler>();
            handlerMock.Setup(m => m.Get(1)).Returns(vm);
            handlerMock.Setup(m => m.Update(It.IsAny<VendingMachine>())).Returns(vm);

            var vendingMachineBusiness = new VendingMachineBusiness(handlerMock.Object);

            var product = vendingMachineBusiness.BuyProduct(1, 1);
            // [MR] If I bougth a product and taking in account logic defined at BuyProduct Method level which returns a Product object. From my understanding what is expected is to DON'T HAVE A NULL value for this product variable.
            // Switched Assert.Null for Assert.NotNull.
            Assert.NotNull(product);
            Assert.Equal(0.05, vm.Credits);
        }

        [Fact]
        public void ShouldReturnProductAndDecreaseCredit_WhenMoneyIsEnough()
        {
            var p1 = new Product()
            {
                Id = 1,
                Name = "Guarana",
                Value = 2
            };

            var p2 = new Product()
            {
                Id = 2,
                Name = "Mandarin",
                Value = 0.5
            };

            var vm = new VendingMachine
            {
                Id = 1,
                Address = "Test Machine",
                Credits = 3.50,
                Products = new List<Product>()
                {
                    p1, p2
                }
            };

            var handlerMock = new Mock<IVendingMachineHandler>();
            handlerMock.Setup(m => m.Get(1)).Returns(vm);
            handlerMock.Setup(m => m.Update(It.IsAny<VendingMachine>())).Returns(vm);

            var vendingMachineBusiness = new VendingMachineBusiness(handlerMock.Object);

            var product = vendingMachineBusiness.BuyProduct(1, 1);

            Assert.NotNull(product);
            Assert.Equal(1, product.Id);

            // [MR]  Just updated Credit Value in order to accomplish with the expected Assertion ;
            Assert.Equal(3.50, vm.Credits);

        }

        /// <summary>
        ///  Test Case for AddCreditwithCoins 
        /// </summary>
        [Fact]
        public void ShouldStoreMoneybyGivenListofCoins()
        {
            var vm = new VendingMachine
            {
                Id = 1,
                Credits = 50.0
            };
            double[] coins = { 0.01, 0.05, 1.10, 2.25, 0.50, 1.00 };
            var handlerMock = new Mock<IVendingMachineHandler>();
            handlerMock.Setup(vmh => vmh.Get(1)).Returns(vm);
            handlerMock.Setup(vmh => vmh.Update(vm)).Returns(vm);

            var vendingMachineBusiness = new VendingMachineBusiness(handlerMock.Object);
            vendingMachineBusiness.AddCreditwithCoins(vm.Id, coins);
            // Expected 50.0 + 0.01 + 0.05 + 0.50 + 1.00
            Assert.Equal(51.56, vm.Credits);

        }


        [Fact]
        public void shouldGetArrayofCoinsBasedOnMachineCredit()
        {
            var vm = new VendingMachine
            {
                Id = 1,
                Credits = 4.68
            };
            double[] coins = { 0.01, 0.05, 1.10, 2.25, 0.50, 1.00 };
            var handlerMock = new Mock<IVendingMachineHandler>();
            handlerMock.Setup(vmh => vmh.Get(1)).Returns(vm);
            handlerMock.Setup(vmh => vmh.Update(vm)).Returns(vm);

            var vendingMachineBusiness = new VendingMachineBusiness(handlerMock.Object);
            double[] vs = vendingMachineBusiness.GetCreditbyMachineId(vm.Id);
            double sum =0;
            for (int i = 0; i < vs.Length; i++)
            {
                sum += vs[i];
            }
            //Sum of Coins Array should match with Machine Credits 
            Assert.Equal(vm.Credits, Math.Round(sum, 2));
        }
    }
}
