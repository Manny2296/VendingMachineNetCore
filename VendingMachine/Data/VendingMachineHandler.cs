using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace VendingMachineApplication.Data
{
    public class VendingMachineHandler : IVendingMachineHandler
    {

        private readonly VendingMachineContext _context;

        public VendingMachineHandler(VendingMachineContext context) 
        {
            _context = context;
        }


        public VendingMachine Get(int id)
        {
            var vendingMachine = _context.VendingMachines.Include("Products")
                .Where(b => b.Id == id)?.FirstOrDefault();

            return vendingMachine;
        }

        public VendingMachine Update(VendingMachine vendingMachine)
        {
            var vending = from machine in _context.VendingMachines
                         where machine.Id == vendingMachine.Id
                          select machine;

            if (vending.Any() == false)
                return null;
    
            var result = _context.VendingMachines.Update(vendingMachine);
            _context.SaveChanges();

            return result.Entity;
        }
    }
}
