using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    public class UserController : Controller
    {
        public static Dictionary<string, string> users = new Dictionary<string, string>();

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPost()
        {
            string email = Request.Form["Email"];
            string password = Request.Form["Password"];
            string ConfirmPassword = Request.Form["ConfirmPassword"];
            
            Console.WriteLine($"Nome: {email} Password: {password}");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult LoginPost()
        {
            string email = Request.Form["Email"];
            string password = Request.Form["Password"];

            Console.WriteLine("Logado!");
            return Content("lOGADO");
        }

    }
}