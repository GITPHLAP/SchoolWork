using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VittighedsMaskinen.Token;

namespace VittighedsMaskinen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]
    public class JokeController : ControllerBase
    {

        [HttpGet]
        [Authorize(Policy = "Dads")]
        [Route("DadJokes")]
        public IActionResult GetDadJoke()
        {

            var test = HttpContext.User;

            if (test.IsInRole("DadJokes"))
            {
                return Ok("Dads");
            }
            else if (test.HasClaim(c => c.Type == "Everything"))
            {
                return Ok("Okay");

            }
            return NotFound("Not implemented");
        }

        [HttpGet]
        [Authorize(Policy = "Everyone")]
        public IActionResult GetJoke(string category)
        {

            // if category is null then try to fetch from cookies
            category ??= Request.Cookies["category"];

            if (category != null)
            {
                CookieOptions co = new CookieOptions();
                co.Expires = DateTimeOffset.Now.AddMonths(2); // using utc so its dosent care if program is travel in timezones
                Response.Cookies.Append("category", category, co);
            }

            string language = GetPreferedLanguage();

            #region Get session jokes
            List<Joke> sessionJokes = HttpContext.Session.GetObjectFromJson<List<Joke>>("jokes");

            sessionJokes ??= new List<Joke>(); // set session jokes to new list of jokes if its null
            #endregion

            Joke joke = GetRandomJoke(sessionJokes, category, 0, language);

            if (joke is null)
            {
                return NotFound("Something went wrong... Maybe you have seen all our jokes - Then try to delete your session");
            }
            //add joke to session list
            sessionJokes.Add(joke);
            HttpContext.Session.SetObjectAsJson("jokes", sessionJokes);

            return Ok(joke);
        }

        private Joke GetRandomJoke(List<Joke> filteredsessionJokes, string category, int accessRight, string language)
        {
            //Dal jokes
            List<Joke> jokes = DAL_Joke.GetJokes(accessRight, category, language);

            //filter all sesion jokes from dal jokes
            List<Joke> avaliableJokes = jokes.Except(filteredsessionJokes, new JokeComparer()).ToList();

            if (avaliableJokes.Count <= 0)
            {
                return null;
            }
            Random rnd = new Random();

            int rndIndex = rnd.Next(avaliableJokes.Count());

            return avaliableJokes[rndIndex];
        }

        private string GetPreferedLanguage()
        {
            string acceptlanguage = Request.GetTypedHeaders().AcceptLanguage?.OrderByDescending(x => x.Quality ?? 1).Select(x => x.Value).FirstOrDefault().ToString();

            if (string.IsNullOrEmpty(acceptlanguage))
            {
                return null;
            }

            CultureInfo culture = new CultureInfo(acceptlanguage);

            return culture.ThreeLetterISOLanguageName;
        }
    }
}
