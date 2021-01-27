using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CocktailApp
{
    public class Liquor
    {
        int id;
        string name;
        int amount;

        public int Id { get => id; set => id = value; }
        public string LiquorName { get => name; set => name = value; }
        public int AmountId { get => amount; set => amount = value; }
        
        //this is a "Foreign key" field its not need to create a foreign key
        //but its need to join the tables.
        public Cocktail Cocktail { get; set; }

        //Constructor
        public Liquor(string name, int amount)
        {
            //this.id = id;
            this.name = name;
            this.amount = amount;
        }

        //default constructor
        public Liquor()
        {

        }


    }
}
