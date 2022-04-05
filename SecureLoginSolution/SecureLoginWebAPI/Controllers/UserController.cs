using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureLoginWebAPI.Models;
using System;

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


        //public void DeleteUser(ApplicationUser user)
        //{

        //}


        //public void FindUserById(string userId)
        //{

        //}

        [HttpGet]
        [Route("FindByName")]
        public ApplicationUser FindByName(string username)
        {
            return Postgres.FindByName(username).Result;
        }


        //public void UpdateUser(ApplicationUser user)
        //{

        //}


        //public void FindByEmail(string email)
        //{

        //}

    }
}
