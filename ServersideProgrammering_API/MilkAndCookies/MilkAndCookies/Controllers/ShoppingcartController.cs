using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkAndCookies.Controllers
{
    [Route("Shoppingcart")]
    [ApiController]
    public class ShoppingcartController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Product> AddProducts([FromQuery]Product newproduct)
        {
            List<Product> vault = HttpContext.Session.GetObjectFromJson<List<Product>>("vault");

            if (vault is null)
            {
                //Set vault equal to new empty list
                vault = new List<Product>();
            }

            //Add parameters from querystring 
            vault.Add(newproduct);

            HttpContext.Session.SetObjectAsJson("vault", vault);

            return vault;
        }
    }
}
