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
        // GET: api/DictatorController
        [HttpGet]
        public async Task<List<Dictator>> Get()
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return await Postgres.GetDictators();
        }

        // POST api/<DictatorController>
        [HttpPost("insert")]
        public Task<Dictator> Post(Dictator newDictator)
        {
            return Postgres.CreateDictator(newDictator);
        }

        // PUT api/<DictatorController>/5
        [HttpPut("update")]
        public void Put(Dictator newDictator)
        {
            Postgres.UpdateDictator(newDictator);
        }

        // DELETE api/<DictatorController>/5
        [HttpDelete()]
        public void Delete(Dictator dictator)
        {
            Postgres.DeleteDictator(dictator);
        }
    }
}
