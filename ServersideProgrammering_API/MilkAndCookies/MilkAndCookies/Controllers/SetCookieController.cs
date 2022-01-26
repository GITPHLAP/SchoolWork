using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MilkAndCookies.Controllers
{
    [Route("SetCookie")]
    [ApiController]
    public class SetCookieController : ControllerBase
    {
        // GET: SetCookie?name=[input]
        [HttpGet]
        public string SetCookie(string name)
        {
            //set the cookie path to this controller 
            CookieOptions co = new CookieOptions();
            //When we set the path to this controller then we tell the cookie which domain it belongs to
            co.Path = "/SetCookie"; 

            Response.Cookies.Append("name", name, co);

            return name;
        }
    }
}
