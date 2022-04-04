using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureLoginWebApp.Models;
using SecureLoginWebApp.Models.DAL;
using System.Threading.Tasks;

namespace SecureLoginWebApp.Controllers
{
    public class SecureLoginController : Controller
    {
        IUserRepository userRepository;

        public SecureLoginController()
        {
            userRepository = new UserRepository();
        }


        //[Route("")]
        //[Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            string username_lower = username.ToLower();

            User user = new User(username_lower, password);
            string msg = await userRepository.ValidateUserCredentials(user);

            if (msg == null)
            {
                HttpContext.Session.SetString("username", username_lower);    
                return View("Success");
            }
            else
            {
                ViewBag.error = msg ;
                return View("Index");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }


        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(string username, string password)
        {
            string username_lower = username.ToLower();
            //Check if username is unique
            if (!await userRepository.IsUserNameUnique(username_lower))
            {
                ViewBag.username = "Username is not unique";
                return View("Index");
            }

            userRepository.CreateUser(new Models.User(username_lower, password));

            //TODO: implement success 
            return RedirectToAction("Index");
        }
    }
}
