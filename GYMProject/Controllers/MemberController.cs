using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMProject.Models;
using GYMProject.Data;

namespace GYMProject.Controllers
{
    public class MemberController : Controller
    {
        private GYMContext db = new GYMContext();

        // GET: Member/Index
        public ActionResult Index()
        {
            var members = db.Members.ToList();
            return View(members);
        }

        // POST: Member/ProcessCommand
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessCommand(string command, Member member)
        {
            switch (command)
            {
                case "Add":
                    db.Members.Add(member);
                    break;
                case "Update":
                    db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                    break;
                case "Delete":
                    db.Entry(member).State = System.Data.Entity.EntityState.Deleted;
                    break;
                default:
                    break;
            }

            db.SaveChanges();

            // Return to index view
            var members = db.Members.ToList();
            return View("Index", members);
        }

        // GET: Member/GetMemberDetails
        public ActionResult GetMemberDetails(int id)
        {
            var member = db.Members.Find(id);
            return Json(member, JsonRequestBehavior.AllowGet);
        }
    }
}
