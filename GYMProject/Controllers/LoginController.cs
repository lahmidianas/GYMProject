using System.Linq;
using System.Web.Mvc;
using System.Web.Security; // Ensure this namespace is included
using GYMProject.Data;
using GYMProject.Models;

namespace GYMProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly GYMContext _context;

        public LoginController()
        {
            _context = new GYMContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                // Simulate successful login
                FormsAuthentication.SetAuthCookie(admin.Username, false);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
