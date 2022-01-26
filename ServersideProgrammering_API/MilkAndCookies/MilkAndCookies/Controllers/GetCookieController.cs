using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkAndCookies.Controllers
{
    [Route("GetCookie")]
    [ApiController]
    public class GetCookieController : ControllerBase
    {
        // GET: GetCookie
        [HttpGet]
        public IActionResult GetCookie()
        {
            string cookieName = Request.Cookies["name"];

            if (cookieName is null)
            {
                return NotFound();
            }

            return Ok(cookieName);
        }
    }
}
