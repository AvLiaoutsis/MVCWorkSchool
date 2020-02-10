using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTryAtWorkSchool.DAL;
using MVCTryAtWorkSchool.Models;

namespace MVCTryAtWorkSchool.Controllers
{
    public class EnrollStudentCourseController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: EnrollStudentCourse
        public ActionResult Index()
        {
            var enrollStudentCourses = db.EnrollStudentCourses.Include(e => e.Course).Include(e => e.Student);
            return View(enrollStudentCourses.ToList());
        }

        // GET: EnrollStudentCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudentCourse enrollStudentCourse = db.EnrollStudentCourses.Find(id);
            if (enrollStudentCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollStudentCourse);
        }

        // GET: EnrollStudentCourse/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            ViewBag.StudentFirstName = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.StudentLastName = new SelectList(db.Students, "StudentID", "LastName");

            return View();
        }

        // POST: EnrollStudentCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollStudentCourseID,CourseID,StudentID,Grade")] EnrollStudentCourse enrollStudentCourse)
        {
            if (ModelState.IsValid)
            {
                db.EnrollStudentCourses.Add(enrollStudentCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollStudentCourse.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollStudentCourse.StudentID);
            return View(enrollStudentCourse);
        }

        // GET: EnrollStudentCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudentCourse enrollStudentCourse = db.EnrollStudentCourses.Find(id);
            if (enrollStudentCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollStudentCourse.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollStudentCourse.StudentID);
            return View(enrollStudentCourse);
        }

        // POST: EnrollStudentCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollStudentCourseID,CourseID,StudentID,Grade")] EnrollStudentCourse enrollStudentCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollStudentCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title", enrollStudentCourse.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", enrollStudentCourse.StudentID);
            return View(enrollStudentCourse);
        }

        // GET: EnrollStudentCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudentCourse enrollStudentCourse = db.EnrollStudentCourses.Find(id);
            if (enrollStudentCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollStudentCourse);
        }

        // POST: EnrollStudentCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollStudentCourse enrollStudentCourse = db.EnrollStudentCourses.Find(id);
            db.EnrollStudentCourses.Remove(enrollStudentCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
