using Microsoft.AspNetCore.Mvc;
using RoadInfoApi.DAL;
using RoadInfoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace RoadInfoApi.Controllers
{
    [Route("api/SituationController")]
    [ApiController]
    public class SituationController : ControllerBase
    {

        // POST: api/SituationController
        [HttpPost]
        public IActionResult Post([Required] double currentLongi, [Required] double currentLati,
            [FromBody] string username, bool forceGet = false)
        {

            List<Situation> situations = GetSituations(currentLongi, currentLati, username, forceGet);
            if (situations == null)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
            else if (situations.Count <= 0)
            {
                return NotFound();

            }

            return new JsonResult(
                situations,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                });
        }

        private List<Situation> GetSituations(double currentLongi, double currentLati, string username, bool forceGet = false)
        {
            Postgres postgres = new Postgres();

            return postgres.GetSituationRecords(postgres.GetSituations(currentLongi, currentLati, username, forceGet).Result).Result;
        }
    }
}
