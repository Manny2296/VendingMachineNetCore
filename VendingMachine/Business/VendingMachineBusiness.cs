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
        private readonly List<Double> ACCEPTED_COINS = new List<Double> { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00 };
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
        private double[] find_subarray(int n, double credits)
        {
            double sum = 0;
            List<Double> resp = new List<double>();
            double diff = 0;
            int rigth = 0;
            int left = n-1; 

            do
            {
                //{ 0.01, 0.05, 0.10, 0.25, 0.50, 1.00 };
                if (ACCEPTED_COINS[rigth] + Math.Round(sum, 2) > credits)
                {
                    rigth++;                
                }
                else if(ACCEPTED_COINS[left] + Math.Round(sum, 2) > credits)
                {
                    left--;
                }
                if(ACCEPTED_COINS[rigth] + Math.Round(sum, 2)  <= credits)
                {
                    sum += Math.Round(ACCEPTED_COINS[rigth],2);
                    resp.Add(ACCEPTED_COINS[rigth]);
                    
                   // diff -= sum;
                    rigth++; 
                }
                if(ACCEPTED_COINS[left] + Math.Round(sum, 2) <= credits)
                {
                    sum += Math.Round(ACCEPTED_COINS[left],2);
                    resp.Add(ACCEPTED_COINS[left]);
                   // diff -= sum;
                    left--;
                }

              
                if(rigth == n-1 && Math.Round(sum,2) != credits)
                {
                   rigth = 0;
                }
                if (left == 0 && Math.Round(sum, 2) != credits)
                {
                    // Whenever Diff is not 0 means that we dont have yet the set of coins for the current credit 
                    // so we need to loop again. 
                    left = n-1;
                }

                if (Math.Round(sum, 2) == credits)
                {
                    return (resp.ToArray());
                }


            } while (credits != Math.Round(sum, 2));
            return (resp.ToArray());
        }
        public double[] GetCreditbyMachineId(int id)
        {
            var vm = _vendingMachineHandler.Get(id);
            var credits = vm.Credits;
            double[] sum_coins = { 1.0 };
            double[] vs = find_subarray(ACCEPTED_COINS.Count, credits);
            
            return vs;
            
        }

        private bool IsValidCoin(double coin)
        {
            
            if (ACCEPTED_COINS.Contains(coin))
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
