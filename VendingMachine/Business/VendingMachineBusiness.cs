using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApplication.Data;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Business
{
    public class VendingMachineBusiness : IVendingMachineBusiness
    {
        private readonly IVendingMachineHandler _vendingMachineHandler;
        private readonly List<Double> accepted_coins = new List<Double> { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00 };
        public VendingMachineBusiness(IVendingMachineHandler vendingMachineHandler)
        {
            _vendingMachineHandler = vendingMachineHandler;
        }

        public VendingMachine AddCredit(int id, double credits)
        {
            var vending = _vendingMachineHandler.Get(id);

            vending.Credits = vending.Credits + credits;

            return _vendingMachineHandler.Update(vending);
        }

        public VendingMachine AddCreditwithCoins(int id, double[] coins)
        {
            double toadd = 0;
            var vending = _vendingMachineHandler.Get(id);

            //Console.WriteLine(len);
            for (int i = 0; i < coins.Length; i++)
            {
                if (IsValidCoin(coins[i]))
                {
                    toadd += coins[i];
                }
            }

            vending.Credits = vending.Credits + toadd;


            return _vendingMachineHandler.Update(vending);
        }

        public Product BuyProduct(int id, int productId)
        {
            var vm = _vendingMachineHandler.Get(id);

            _vendingMachineHandler.Update(vm);

            return vm.Products.First(p => p.Id == productId);
        }

        public double[] getCreditbyMachineId(int id)
        {
            throw new NotImplementedException();
        }

        private bool IsValidCoin(double coin)
        {
            
            if (accepted_coins.Contains(coin))
            {
                return true;
               
            }
            else
            {
                return false;
            }
           
        }
    }
}
