using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailApp
{
    public class Program
    {
        //TODO: change stuff in class diagram
        //TODO: Test everyting
        static void Main(string[] args)
        {
            CreateTestData();

            Bar bestbar = new Bar();

            RunTestData(bestbar);

            #region UI
            while (true)
            {

                //Clear console windows
                Console.Clear();

                Console.WriteLine("What do you want to do?");
                Console.WriteLine("Press E: for Edit Cocktail");
                Console.WriteLine("Press F: for Find Cocktail");
                Console.WriteLine("Press C: for Create Cocktail");
                Console.WriteLine("Press G: for Get drink");
                Console.WriteLine("Press D: for Delete");


                //Read userinput
                string userinput = Console.ReadLine();

                //switch case on what user had pick
                switch (userinput)
                {
                    case "E":
                        Console.WriteLine("Write new values");
                        bestbar.EditCocktail(CreateCocktailObj());
                        break;
                    case "F":
                        Console.WriteLine("Write what you search after");
                        string searchstr = Console.ReadLine();
                        bestbar.FindCocktail(searchstr);
                        ShowCocktailToUser(bestbar.Cocktails);
                        break;
                    case "C":
                        Console.WriteLine("Create new Cocktail");
                        bestbar.CreateCocktail(CreateCocktailObj());
                        break;
                    case "D":
                        Console.WriteLine("write name on the Cocktail you want to delete");
                        string cocktailname = Console.ReadLine();
                        bestbar.DeleteCocktail(cocktailname);
                        break;
                    case "G":
                        Console.WriteLine("write name on the Cocktail you want");
                        string get_cocktailname = Console.ReadLine();
                        bestbar.GetCocktail(get_cocktailname);
                        ShowCocktailToUser(bestbar.Cocktails);
                        break;
                    case "exit":
                        Environment.Exit(1);
                        break;
                    default:
                        break;
                }

                Console.ReadLine();
            }

            #endregion

        }

        private static Cocktail CreateCocktailObj()
        {
            //Create list for liquords
            List<Liquor> liquors = new List<Liquor>(); 

            Console.WriteLine("Write name on Cocktail");
            string name = Console.ReadLine();

            Console.WriteLine("Write all additions in Cocktail, seperate with , ");
            string additions = Console.ReadLine();

            Console.WriteLine("Write number of liquords in Cocktail");
            Console.WriteLine("max 5");
            int liquordsnumber = Convert.ToInt32(Console.ReadLine());

            //Create a liquord and add it the amount of times user had specified
            for (int i = 1; i <= liquordsnumber; i++)
            {
                Console.WriteLine("Write name on liquord");
                string liquordname = Console.ReadLine(); // read input
                Console.WriteLine("Write amount of liquord");
                int liquordamount = Convert.ToInt32(Console.ReadLine()); //convert input to int

                //add liquords to list
                liquors.Add(new Liquor(liquordname, liquordamount));
            }

            return new Cocktail(name, liquors, additions);

        }

        private static void CreateTestData()
        {
            //Jeg er godt klar over mine test variabler ikke er pæne
            //men klokken blev 19:45 så gad ikke side med det mere 
            using (var ctx = new BarContext())
            {
                Liquor wrum_Liquor = new Liquor("White Rum2", 40); //create a new liquor to Cocktail
                Liquor wrum_Liquor12 = new Liquor("Soda", 20);

                List<Liquor> liquorlist = new List<Liquor>(); //create a list with liquor

                liquorlist.Add(wrum_Liquor12); //add liquor to list
                liquorlist.Add(wrum_Liquor);

                string additionslist = "Mint, Sugar"; // add additions to cocktail


                Liquor wrum_Liquor2 = new Liquor("White Rum1", 20);

                List<Liquor> liquorlist2 = new List<Liquor>();

                liquorlist2.Add(wrum_Liquor2);

                string additionslist2 = "test";


                Liquor wrum_Liquor3 = new Liquor("White Rum1", 30);

                List<Liquor> liquorlist3 = new List<Liquor>();

                liquorlist3.Add(wrum_Liquor3);

                string additionslist3 = "test3";


                Cocktail drink3 = new Cocktail("Mojito3", liquorlist3, additionslist3); //create cocktail
                Cocktail drink = new Cocktail("Mojito2", liquorlist, additionslist);
                Cocktail drink2 = new Cocktail("Mojito1", liquorlist2, additionslist2);

                //add cocktails to database 
                ctx.Cocktails.Add(drink);
                ctx.Cocktails.Add(drink2);
                ctx.Cocktails.Add(drink3);

                //save what I had created
                ctx.SaveChanges();

            }
        }

        private static void RunTestData(Bar bestbar)
        {

            bestbar.GetCocktails();
            ShowCocktailToUser(bestbar.Cocktails);
            Console.WriteLine("-------------------------------");

            Liquor wrum_Liquor3new = new Liquor("White Rum5", 200);
            List<Liquor> liquorlist3new = new List<Liquor>();
            liquorlist3new.Add(wrum_Liquor3new);
            string additionslist3new = "test";

            bestbar.EditCocktail(new Cocktail("Mojito3", liquorlist3new, additionslist3new));
            Console.WriteLine("-------------------------------");

            bestbar.GetCocktails();
            ShowCocktailToUser(bestbar.Cocktails);

            Console.WriteLine("-------------------------------");

            bestbar.DeleteCocktail("Mojito3");

            bestbar.GetCocktails();
            ShowCocktailToUser(bestbar.Cocktails);




            Console.WriteLine("----------------FIND---------------");

            bestbar.FindCocktail("test");
            ShowCocktailToUser(bestbar.Cocktails);
        }

        private static void ShowCocktailToUser(List<Cocktail> cocktails)
        {
            foreach (var item in cocktails)
            {
                Console.WriteLine();
                Console.WriteLine("Name:    " + item.CocktailNameId);
                Console.WriteLine("Additions:    " + item.Additions);
                Console.WriteLine("Liquords:    ");


                foreach (var item2 in item.Liquors)
                {
                    Console.Write(item2.LiquorName);
                    Console.Write("    ");
                    Console.Write(item2.AmountId + "ml");
                }

            }
        }

    }
}
