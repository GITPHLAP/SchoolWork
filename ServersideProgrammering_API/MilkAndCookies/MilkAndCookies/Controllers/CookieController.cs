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
            //Set a new cookie with input
            Response.Cookies.Append("favoritMilkshake", favMilkshake);
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
