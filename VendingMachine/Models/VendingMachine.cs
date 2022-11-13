using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachineApplication.Models
{
    public class VendingMachine
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Credits { get; set; } 
        public double[] Coins { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

}
