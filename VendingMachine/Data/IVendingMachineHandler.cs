using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Data
{
    public interface IVendingMachineHandler
    {
        VendingMachine Get(int id);

        VendingMachine Update(VendingMachine vendingMachine);
    }
}
