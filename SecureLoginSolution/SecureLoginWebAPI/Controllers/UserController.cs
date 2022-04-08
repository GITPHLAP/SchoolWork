using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureLoginWebAPI.Models;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("new")]
        public IActionResult CreateUser(ApplicationUser user)
        {
            try
            {
                Postgres.CreateUser2(user);
                return Ok("Created user");
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }

        }

        [HttpDelete]
        public IActionResult DeleteUser(int userid)
        {
            try
            {
                Postgres.DeleteUser(userid);
                return Ok("Deleted user");
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("FindById")]
        public async Task<IActionResult> FindUserById(string id)
        {

            if (await Postgres.FindById(id) is ApplicationUser user)
            {
                return Ok(user);
            }

            return NotFound();

        }

        [HttpGet]
        [Route("FindByName")]
        public async Task<IActionResult> FindByName(string username)
        {
            if (await Postgres.FindByName(username) is ApplicationUser user)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(ApplicationUser user)
        {
            try
            {
                Postgres.UpdateUser(user);
                return Ok("updated user");
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("FindByEmail")]
        public async Task<IActionResult> FindByEmail(string email)
        {
            if (await Postgres.FindByEmail(email) is ApplicationUser user)
            {
                return Ok(user);
            }

            return NotFound();

        }

    }
}
