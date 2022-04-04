using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureLoginWebAPI.Models;

namespace SecureLoginWebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            Postgres.CreateUser(user);
            return Ok("Created user");
        }

        [Route("{username}")]
        [HttpGet]
        public bool IsUserNameUnique(string username)
        {

            return Postgres.IsUsernameUnique(username).Result;
        }


    }
}
