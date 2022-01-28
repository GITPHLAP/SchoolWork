using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VittighedsMaskinen.Token;

namespace VittighedsMaskinen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticateController : ControllerBase
    {


        private readonly IJwtAuth jwtAuth;

        public AuthenticateController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] Credentials crendential)
        {
            var token = jwtAuth.Authentication(crendential);
            if (token is null)
            {
                return Unauthorized("Your credentials does not match a valid user");
            }
            //Save token in session
            HttpContext.Session.SetObjectAsJson("Token", token);

            return Ok(token);
        }
    }
}
