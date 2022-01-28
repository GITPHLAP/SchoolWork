using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategoryList()//string apikey)
        {
            List<string> catoList = DAL_Joke.GetJokes(0).Select(j => j.Category).Distinct().ToList();
            return Ok(catoList);
        }
    }
}
