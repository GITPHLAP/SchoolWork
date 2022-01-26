using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MilkAndCookies.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeleteProductController : ControllerBase
    {


        [HttpDelete]
        public IActionResult DeleteProductFromCartController([FromQuery]Product product)
        {
            //Get vault 
            List<Product> vault = HttpContext.Session.GetObjectFromJson<List<Product>>("vault");

            //Validate vault
            if (vault is null)
            {
                return NotFound("Vault not found");
            }
            //Check if the input match a product in the session
            if (!vault.Any(p => p.Name == product.Name && p.Price == product.Price))
            {
                return NotFound("Product not found");
            }
            vault.RemoveAll(p => p.Name == product.Name && p.Price == product.Price);
            return Ok();
        }
    }
}
