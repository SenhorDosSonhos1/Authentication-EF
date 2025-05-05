using Authentication.Data;
using Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Authentication.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

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
            
            if (password != ConfirmPassword)
            {
                ViewBag.Mensagem = "As senhas não coincidem.";
                return View("Register");
            }

            var newUser = new User {Email = email, Password = password};

            _context.Users.Add(newUser);
            _context.SaveChanges();

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

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("usuarioLogado", user.Email);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.Mensagem = "Usuario ou senha inválidos.";
                return View("Login");
                //Console.WriteLine("teste");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}