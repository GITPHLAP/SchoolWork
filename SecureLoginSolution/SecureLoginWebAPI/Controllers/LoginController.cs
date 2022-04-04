using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureLoginWebAPI.Models;

namespace SecureLoginWebAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (Postgres.ValidateUserCrendentials(user).Result)
            {
                return Ok();
            }
            
            return Unauthorized("Forkert password eller brugernavn");
        }
    }
}
