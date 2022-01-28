using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen
{
    public class DAL_Joke
    {
        //TODO: create generic class ex. result with ok and fail then it can return a message if it fail and else return type
        public static List<Joke> GetJokes(int rights, string category = null, string language = null)
        {
            #region Create the list with jokes
            List<Joke> jokes = new List<Joke>();

            jokes.Add(new Joke { Id = 1, Category = "AllKids", Text = "Alle børnene gik glad gennem svingdøren undtagen Mikkel han fik mast sin testikel XD", Language="dan" });

            jokes.Add(new Joke { Id = 2, Category = "YourMotherJokes", Text = "Din mor er så fed, at når hun svømmer en tur i stillehavet synger hvalerne “We are family!”", Language = "dan" });

            jokes.Add(new Joke
            {
                Id = 3,
                Category = "KnockKnock",
                Text = "Knock, knock. Who’s there ? Canoe.Canoe who ? Canoe help me with my homework ?", Language="eng" });
            
            jokes.Add(new Joke { Id = 4, Category = "AllKids", Text = "Alle børnene kom sikkert ind fra sneboldkampen – Undtagen Asger han fik en vasker", Language = "dan" });

            jokes.Add(new Joke { Id = 5, Category = "DadJokes", Text = "What did the ocean say to the beach? Nothing, it just waved", Language = "eng" });

            jokes.Add(new Joke { Id = 6, Category = "DadJokes", Text = "I only know 25 letters of the alphabet. I don´t know y", Language = "eng" });

            #endregion

            #region filter jokes this should database do
            List<Joke> filteredJokes = new List<Joke>();
            switch (rights)
            {
                case 0:
                    filteredJokes = jokes;
                    break;
                case 1:
                    filteredJokes = jokes.Where(j => j.Category == "DadJokes").ToList();
                    break;
                default: //This should never happend
                    return new List<Joke>();
            }

            if (category is string)
            {
                filteredJokes = filteredJokes.Where(j => j.Category == category).ToList();
            }

            if (language is string)
            {
                filteredJokes = filteredJokes.Where(j => j.Language == language).ToList();
            }


            #endregion
            //Add more jokes

            return filteredJokes;
        }

    }
}
