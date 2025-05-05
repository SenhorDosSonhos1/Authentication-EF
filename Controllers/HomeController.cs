using Microsoft.AspNetCore.Mvc;
using Authentication.Models;

namespace HomeController.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("usuarioLogado");

            if (user == null)
                return RedirectToAction("Register", "User");
            
            return View();
        }
    }
}