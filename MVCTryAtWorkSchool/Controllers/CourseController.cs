using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCTryAtWorkSchool.DAL;
using MVCTryAtWorkSchool.Models;
using PagedList;

namespace MVCTryAtWorkSchool.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Course
        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.CourseIDSortParm = sortOrder == "CourseID" ? "courseID_desc" : "CourseID";
            ViewBag.StreamSortParm = sortOrder == "Stream" ? "stream_desc" : "Stream";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.StartDateSortParm = sortOrder == "Start" ? "start_desc" : "Start";
            ViewBag.EndDateSortParm = sortOrder == "End" ? "end_desc" : "End";
            ViewBag.DepartmentSortParm = sortOrder == "Department" ? "Department_desc" : "Department";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var courses = db.Courses.Include(c => c.Department);

            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.Title.Contains(searchString)
                                          || s.Stream.Contains(searchString)
                                          || s.Type.Contains(searchString)
                                         // || s.TuitionFees.ToString().Contains(searchString)
                                         );
            }

            switch (sortOrder)
            {
                case "title_desc":
                    courses = courses.OrderByDescending(s => s.Title);
                    break;
                case "CourseID":
                    courses = courses.OrderBy(s => s.CourseID);
                    break;
                case "CourseID_desc":
                    courses = courses.OrderByDescending(s => s.CourseID);
                    break;
                case "Stream":
                    courses = courses.OrderBy(s => s.Stream);
                    break;
                case "stream_desc":
                    courses = courses.OrderByDescending(s => s.Stream);
                    break;
                case "Type":
                    courses = courses.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    courses = courses.OrderByDescending(s => s.Type);
                    break;
                case "Start":
                    courses = courses.OrderBy(s => s.StartDate);
                    break;
                case "start_desc":
                    courses = courses.OrderByDescending(s => s.StartDate);
                    break;
                case "End":
                    courses = courses.OrderBy(s => s.EndDate);
                    break;
                case "end_desc":
                    courses = courses.OrderByDescending(s => s.EndDate);
                    break;
                case "Deparment":
                    courses = courses.OrderBy(s => s.Department);
                    break;
                case "Deparment_desc":
                    courses = courses.OrderByDescending(s => s.Department);
                    break;
                default:
                    courses = courses.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(courses.ToPagedList(pageNumber, pageSize));
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Stream,Type,StartDate,EndDate,DepartmentID")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var courseToUpdate = db.Courses.Find(id);
            if (TryUpdateModel(courseToUpdate, "", new string[] { "Title","Stream","Type","StartDate","EndDate","DepartmentID" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
            return View(courseToUpdate);
        }
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentQuery = from d in db.Departments
                                  orderby d.Name
                                  select d;

            ViewBag.DepartmentID = new SelectList(departmentQuery, "DepartmentID", "Name", selectedDepartment);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Course courseToDelete = new Course() { CourseID = id };
                db.Entry(courseToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangersError = true });
            }
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
