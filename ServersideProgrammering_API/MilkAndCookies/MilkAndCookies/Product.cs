using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MilkAndCookies
{
    public class Product
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }

        //Default constructor 
        public Product()
        {

        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
