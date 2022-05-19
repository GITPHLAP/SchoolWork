using DictatorTweets_API.DAL;
using DictatorTweets_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DictatorTweets_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictatorController : ControllerBase
    {
        IDictatorDB db;
        public DictatorController(IDictatorDB _db)
        {
            db = _db;
        }
        // GET: api/DictatorController
        [HttpGet]
        public async Task<List<Dictator>> Get()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return await db.GetDictators();
        }

        // POST api/<DictatorController>
        [HttpPost("insert")]
        public Task<Dictator> Post(Dictator newDictator)
        {
            return db.CreateDictator(newDictator);
        }

        // PUT api/<DictatorController>/5
        [HttpPut("update")]
        public void Put(Dictator newDictator)
        {
            db.UpdateDictator(newDictator);
        }

        // DELETE api/<DictatorController>/5
        [HttpDelete()]
        public void Delete(Dictator dictator)
        {
            db.DeleteDictator(dictator);
        }
    }
}
