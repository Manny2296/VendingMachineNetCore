using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApplication.Models;

namespace VendingMachineApplication.Models
{
    public class DataSeed
    {

        private readonly VendingMachineContext _context;

        public DataSeed(VendingMachineContext context)
        {
            _context = context;
        }

        public void RunSeed()
        {
            // _context.Database.EnsureCreated();

            var vm1 = _context.VendingMachines.FirstOrDefault(vm => vm.Address == "Golden Gate");
            if (vm1 == null)
            {
                _context.VendingMachines.Add(new VendingMachine
                {
                    Address = "Golden Gate",
                    Products = new List<Product>()
                        {
                            new Product()
                            {
                                Name = "Guarana",
                                Value = 1,
                                Quantity = 5
                            },
                            new Product()
                            {
                                Name = "Bergamota",
                                Value = 0.5,
                                Quantity = 5
                            }
                        }

                });
            }

            var vm2 = _context.VendingMachines.FirstOrDefault(vm => vm.Address == "Statue of Liberty");
            if (vm2 == null)
            {
                _context.VendingMachines.Add(new VendingMachine
                {
                    Address = "Statue of Liberty",
                    Products = new List<Product>()
                        {
                            new Product()
                            {
                                Name = "Coke",
                                Value = 2,
                                Quantity = 5
                            },
                            new Product()
                            {
                                Name = "Apple",
                                Value = 0.5,
                                Quantity = 5
                            }
                        }

                });
            }

            _context.SaveChanges();
        }
    }
}
