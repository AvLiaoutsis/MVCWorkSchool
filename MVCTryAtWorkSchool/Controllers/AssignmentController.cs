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
using PagedList;

namespace MVCTryAtWorkSchool.Controllers
{
    public class AssignmentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Assignment
        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortAsParm = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DescriptionSortAsParm = sortOrder == "Desc" ? "desc_desc" : "Desc";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var assignments = from s in db.Assignments
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                assignments = assignments.Where(s => s.Title.Contains(searchString)
                                          || s.Description.Contains(searchString)
                                         );
            }

            switch (sortOrder)
            {
                case "title_desc":
                    assignments = assignments.OrderByDescending(s => s.Title);
                    break;
                case "Desc":
                    assignments = assignments.OrderBy(s => s.Description);
                    break;
                case "desc_desc":
                    assignments = assignments.OrderByDescending(s => s.Description);
                    break;
                default:
                    assignments = assignments.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(assignments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Assignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Assignment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description")] Assignment assignment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Assignments.Add(assignment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(assignment);
        }

        // GET: Assignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignment/Edit/5
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
            var assignmentToUpdate = db.Assignments.Find(id);
            if (TryUpdateModel(assignmentToUpdate, "", new string[] { "Title", "Description" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(assignmentToUpdate);
        }

        // GET: Assignment/Delete/5
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
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Assignment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Assignment assignmentToDelete = new Assignment() { AssignmentID = id };
                db.Entry(assignmentToDelete).State = EntityState.Deleted;
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
