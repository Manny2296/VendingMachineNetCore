using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Business
{
    public interface IVendingMachineBusiness
    {
        public VendingMachine AddCredit(int id, double credits);

        public VendingMachine AddCreditwithCoins(int id, double[] coins);
        Product BuyProduct(int id, int productId);

        public double[] GetCreditbyMachineId(int id);
     }
}
