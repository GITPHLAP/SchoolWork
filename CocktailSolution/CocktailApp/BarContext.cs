using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CocktailApp
{
    public class BarContext : DbContext
    {
        //I could also give database a custom name in base 
        public BarContext(): base()
        {
            //Drop database and create a new every time
            Database.SetInitializer<BarContext>(new DropCreateDatabaseAlways<BarContext>());  
        }

        //this should be tables in database
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Liquor> Liquors { get; set; }

    }
}
