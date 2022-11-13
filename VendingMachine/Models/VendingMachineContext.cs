using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachineApplication.Models
{
    public class VendingMachineContext : DbContext
    {
        public DbSet<Product> Products{ get; set; }

        public DbSet<VendingMachine> VendingMachines { get; set; }

        public VendingMachineContext(DbContextOptions<VendingMachineContext> options) :
           base(options)
        { }
    }
}
