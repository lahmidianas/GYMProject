using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using GYMProject.Models;
using GYMProject.Repositories;
using GYMProject.Data;

namespace GYMProject.Controllers
{
    public class SessionController : Controller
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Staff> _staffRepository;
        private readonly GYMContext _context; // Add GYMContext field

        public SessionController(IRepository<Session> sessionRepository, IRepository<Staff> staffRepository, GYMContext context)
        {
            _sessionRepository = sessionRepository;
            _staffRepository = staffRepository;
            _context = context; // Inject GYMContext here
        }

        public ActionResult Index()
        {
            var sessions = _sessionRepository.GetAll().ToList();

            var coaches = _staffRepository.GetAll()
                .Where(s => s.Job.ToLower() == "coach")
                .Select(s => new SelectListItem { Value = s.StaffId.ToString(), Text = s.Nom + " " + s.Prenom })
                .ToList();

            var membershipTypes = new List<string> { "full-contact", "taekwandoo", "boxing", "bodybuilding" };
            var sessionTypes = membershipTypes.Select(m => new SelectListItem { Value = m, Text = m }).ToList();

            var viewModel = new SessionViewModel
            {
                Sessions = sessions,
                NewSession = new Session(),
                Coaches = coaches,
                SessionTypes = sessionTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(SessionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var session = viewModel.NewSession;
                session.Staff = _staffRepository.GetById(session.StaffId);
                _sessionRepository.Add(session);
                _sessionRepository.SaveChanges(); // Save changes after adding
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        [HttpPost]
        public ActionResult Update(Session session)
        {
            if (ModelState.IsValid)
            {
                session.Staff = _staffRepository.GetById(session.StaffId);
                _sessionRepository.Update(session);
                _sessionRepository.SaveChanges(); // Save changes after updating
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_sessionRepository.Exists(id))
            {
                _sessionRepository.Delete(id);
                _sessionRepository.SaveChanges(); // Save changes after deleting
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public JsonResult Details(int id)
        {
            var session = _sessionRepository.GetById(id);
            if (session == null)
            {
                return Json(new { success = false, message = "Session not found" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, session }, JsonRequestBehavior.AllowGet);
        }
    }
}
