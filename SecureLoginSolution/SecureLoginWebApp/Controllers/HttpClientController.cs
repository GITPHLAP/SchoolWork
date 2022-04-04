using Microsoft.AspNetCore.Mvc;

namespace SecureLoginWebApp.Controllers
{
    public class HttpClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
