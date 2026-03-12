using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GYMProject.Data;
using GYMProject.Security;

namespace GYMProject.Controllers
{
    [AllowAnonymous]
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
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username);

            if (admin != null && PasswordHasher.VerifyPassword(password, admin.PasswordHash, admin.PasswordSalt, admin.PasswordIterations))
            {
                FormsAuthentication.SetAuthCookie(admin.Username, false);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
