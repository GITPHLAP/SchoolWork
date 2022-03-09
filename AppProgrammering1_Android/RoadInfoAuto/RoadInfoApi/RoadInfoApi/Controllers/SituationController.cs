using Microsoft.AspNetCore.Mvc;
using RoadInfoApi.DAL;
using RoadInfoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoadInfoApi.Controllers
{
    [Route("api/SituationController")]
    [ApiController]
    public class SituationController : ControllerBase
    {
        // GET: api/SituationController
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(
                GetSituations(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                });
        }

        // GET api/<SituationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SituationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SituationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SituationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        private List<Situation> GetSituations()
        {
            Postgres postgres = new Postgres();

            return postgres.GetSituationRecords(postgres.GetSituations().Result).Result;
        }
    }
}
