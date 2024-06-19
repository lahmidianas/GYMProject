using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GYMProject.Models;
using GYMProject.Repositories;
using GYMProject.Data;

namespace GYMProject.Controllers
{
    public class StaffController : Controller
    {
        private readonly IRepository<Staff> _staffRepository;

        public StaffController()
        {
            _staffRepository = new Repository<Staff>(new GYMContext());
        }

        // GET: Staff
        public ActionResult Index()
        {
            var staffList = _staffRepository.GetAll();
            return View(staffList);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _staffRepository.Add(staff);
                _staffRepository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // POST: Staff/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<int> staffIds)
        {
            if (staffIds == null || staffIds.Count == 0)
            {
                return HttpNotFound();
            }

            foreach (var id in staffIds)
            {
                var staff = _staffRepository.GetById(id);
                if (staff != null)
                {
                    _staffRepository.Delete(id);
                }
            }
            _staffRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        // AJAX: Get staff details
        public ActionResult GetStaffDetails(int id)
        {
            var staff = _staffRepository.GetById(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return Json(staff, JsonRequestBehavior.AllowGet);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = _staffRepository.GetById(id.Value);
            if (staff == null)
            {
                return HttpNotFound();
            }

            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _staffRepository.Update(staff);
                _staffRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staff);
        }
    }
}
