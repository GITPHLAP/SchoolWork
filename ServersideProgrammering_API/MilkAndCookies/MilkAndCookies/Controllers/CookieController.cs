using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkAndCookies.Controllers
{
    [ApiController]
    [Route("Cookie")]
    public class CookieController : ControllerBase
    {

        //Get: Cookie?favMilkshake=[input]
        [HttpGet]
        public string Get(string favMilkshake)
        {
            //Create cookie expires after 5 minutes option 
            CookieOptions co = new CookieOptions();
            co.Expires = DateTimeOffset.Now.AddMinutes(5); // using utc so its dosent care if program is travel in timezones


            //Set a new cookie with input
            Response.Cookies.Append("favoritMilkshake", favMilkshake, co);
            return favMilkshake;
        }

        // GET: Cookie/MilkshakeFromCookie
        [Route("[action]")]
        [HttpGet]
        public string GetMilkshakeFromCookie()
        {
            //Reguest cookie from client
            string milkshake = Request.Cookies["favoritMilkshake"];
            
            if (milkshake is null)
            {
                return "unknown";
            }

            return milkshake;
        }

    }
}
