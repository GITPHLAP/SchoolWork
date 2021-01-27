using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CocktailApp
{
    public class Bar
    {

        List<Cocktail> cocktails = new List<Cocktail>();
        public List<Cocktail> Cocktails { get => cocktails; set => cocktails = value; } //Cocktail list in the bar 

        public void GetCocktails()
        {
            Cocktails.Clear(); // clear the Cocktail-card because its need to be refresed 
            using (var ctx= new BarContext()) // Barcontext to connect to database
            {
                //Query where i include Liquors in cocktails
                //here I include liquors when in query result else liquor will be empty
                var query = ctx.Cocktails.Include(l => l.Liquors); 

                foreach (var item in query)
                {
                    cocktails.Add(item); // add what i found to cocktail-card
                }
            }

        }

        public Cocktail GetCocktail(string searchstr)
        {
            //Cocktails.Clear();
            using (var ctx = new BarContext())
            {
                //Query where I take the first of the statement
                //include Liquors in cocktails and cocktailname match search parameter
                Cocktail cocktail = ctx.Cocktails.Include(l => l.Liquors).Where(c => c.CocktailNameId == searchstr).First();
                return cocktail;
            }
        }

        public void DeleteCocktail(string cocktailname)
        {
            using (var ctx = new BarContext())
            {
                //Query where I take the first of the statement
                //include Liquors in cocktails and cocktailname match search parameter
                var cocktail = ctx.Cocktails.Where(c => c.CocktailNameId == cocktailname).Include(l => l.Liquors).First();

                //if there is a cocktail then do it. 
                if (cocktail != null)
                {
                    //first I start to remove liquors because I can't remove 
                    //them after because its a foreign key on Cocktails
                    foreach (var item in cocktail.Liquors.ToList())
                    {
                        //Remove them after i have found them by the id
                        ctx.Liquors.Remove(ctx.Liquors.Find(item.Id));
                    }
                    ctx.SaveChanges(); // save changes

                    ctx.Cocktails.Remove(cocktail); // remove cocktail 

                    ctx.SaveChanges(); // save changes again
                }

            }

        }

        public void CreateCocktail(Cocktail cocktail)
        {
            using (var ctx = new BarContext())
            {
                // add the cocktail user had made to database
                ctx.Cocktails.Add(cocktail);
                ctx.SaveChanges(); // save the created cocktail
            }
        }

        public void EditCocktail(Cocktail newcocktail)
        {
            using (var ctx = new BarContext())
            {
                //Query where I take the first of the statement
                //include Liquors in cocktails and cocktailname match search parameter
                var oldcocktail = ctx.Cocktails.Where(c => c.CocktailNameId == newcocktail.CocktailNameId).Include(l => l.Liquors).First();
                
                if (oldcocktail != null)
                {
                    //remove the liquors first
                    foreach (var item in oldcocktail.Liquors.ToList())
                    {
                        ctx.Liquors.Remove(ctx.Liquors.Find(item.Id));
                    }
                    ctx.SaveChanges(); //save that its removed

                    //then add the new liquors and additions to the old on
                    oldcocktail.Liquors = newcocktail.Liquors;
                    oldcocktail.Additions = newcocktail.Additions;

                    //save it again so it saved the old cocktail just with updated values
                    ctx.SaveChanges(); 
                }

            }

        }

        public void FindCocktail(string searchstr)
        {
            Cocktails.Clear(); //clear cocktail card
            using (var ctx = new BarContext())
            {
                //inner join between Cooktails and liquors
                //I can do this because liquor know which cocktail it connected to
                var query = ctx.Cocktails.Join(ctx.Liquors,
                        cocktail => cocktail.CocktailNameId,
                        liquor => liquor.Cocktail.CocktailNameId,
                        (cocktail, liquor) => new
                            {
                             liquor,
                             cocktail
                            }
                        ).Where(r => r.cocktail.Additions.Contains(searchstr) || r.liquor.LiquorName == searchstr);
                //Where cocktail additions contains searchstr or liquor name is the same as searchstr

                foreach (var item in query)
                {
                    cocktails.Add(item.cocktail); //add the result to the cocktail card
                }
                
            }

        }


    }
}
