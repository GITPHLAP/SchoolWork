using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CocktailApp
{
    public class Cocktail
    {
        string name;
        List<Liquor> liquors = new List<Liquor>();
        string additions;

        [Key] // create this as a primary key
        public string CocktailNameId { get => name; set => name = value; }
        public List<Liquor> Liquors { get => liquors; set => liquors = value; }
        public string Additions { get => additions; set => additions = value; }

        //Constructor
        public Cocktail(string name, List<Liquor> liquors, string additions)
        {
            this.name = name;
            this.liquors = liquors;
            this.additions = additions;
        }

        //default constructor
        public Cocktail()
        {

        }

    }
}
