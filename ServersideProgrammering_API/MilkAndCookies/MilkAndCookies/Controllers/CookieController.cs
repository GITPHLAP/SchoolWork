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


        [HttpGet]
        public string Get(string favMilkshake)
        {

        }
    }
}
