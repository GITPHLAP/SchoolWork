using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureLoginWebAPI.Models;

namespace SecureLoginWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateRole(ApplicationRole role)
        {
            return Ok(role);
        }


        //public void UpdateRole(ApplicationRole role)
        //{

        //}



        //public void DeleteRole(ApplicationRole role)
        //{

        //}


        //public void FindById(string roleId)
        //{

        //}


        //public void FindByName(string rolename)
        //{

        //}

    }
}
