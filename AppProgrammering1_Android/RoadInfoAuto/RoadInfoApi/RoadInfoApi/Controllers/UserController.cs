using Microsoft.AspNetCore.Mvc;
using RoadInfoApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoadInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] string username)
        {
            return CreateUser(username);
        }

        private IActionResult CreateUser(string username)
        {
            Postgres postgres = new Postgres();
            var con = postgres.GetConnection();

            if (postgres.CheckIfUsernameIsValid(con, username).Result)
            {
                postgres.CreateOrUpdateUserActivity(con, username).Wait();

                postgres.CloseConnection(con);

                return Ok("User is added");
            }

            return Conflict($"The username: {username} is already exist in db please use another ");
        }

    }
}
