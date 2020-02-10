using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTryAtWorkSchool.DAL;
using MVCTryAtWorkSchool.ViewModels;

namespace MVCTryAtWorkSchool.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<StudentByDate> data = from student in db.Students
                                             group student by student.DateOfBirth into dateGroup
                                             select new StudentByDate()
                                             {
                                                 BirthDate = dateGroup.Key,
                                                 StudentCount = dateGroup.Count()
                                             };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

}