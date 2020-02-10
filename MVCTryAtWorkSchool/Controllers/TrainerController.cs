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
using MVCTryAtWorkSchool.ViewModels;
using PagedList;


namespace MVCTryAtWorkSchool.Controllers
{
    public class TrainerController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Trainer
        public ActionResult IndexWithoutViewBag(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "Last" ? "last_desc" : "Last";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.SubjectSortParm = sortOrder == "Subject" ? "subj_desc" : "Subject";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var trainers = from s in db.Trainers
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                trainers = trainers.Where(s => s.FirstName.Contains(searchString)
                                          || s.LastName.Contains(searchString)
                                         //|| s.DateOfBirth.ToString().Contains(searchString)
                                          || s.Subject.Contains(searchString)
                                         );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    trainers = trainers.OrderByDescending(s => s.FirstName);
                    break;
                case "Last":
                    trainers = trainers.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    trainers = trainers.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    trainers = trainers.OrderBy(s => s.DateOfBirth);
                    break;
                case "date_desc":
                    trainers = trainers.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "Subject":
                    trainers = trainers.OrderBy(s => s.Subject);
                    break;
                case "subj_desc":
                    trainers = trainers.OrderByDescending(s => s.Subject);
                    break;
                default:
                    trainers = trainers.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(trainers.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new TrainerIndexData();

            viewModel.Trainers = db.Trainers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.TrainerID = id.Value;
                viewModel.Courses = viewModel.Trainers.Where(
                    i => i.TrainerID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                db.Entry(selectedCourse).Collection(x => x.EnrollStudentCourses).Load();
                foreach (EnrollStudentCourse enrollment in selectedCourse.EnrollStudentCourses)
                {
                    db.Entry(enrollment).Reference(x => x.Student).Load();
                }

                viewModel.enrollStudentCourses = selectedCourse.EnrollStudentCourses;
            }

            return View(viewModel);
        }
        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            var trainer = new Trainer();
            trainer.Courses = new List<Course>();
            PopulateAssignedCourseData(trainer);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,DateOfBirth,Subject,OfficeAssignment")]Trainer trainer, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                trainer.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    trainer.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(trainer);
            return View(trainer);
        }

        // GET: Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.TrainerID == id)
                .Single();
            PopulateAssignedCourseData(trainer);

            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }
        private void PopulateAssignedCourseData(Trainer trainer)
        {
            var allCourses = db.Courses;
            var trainerCourses = new HashSet<int>(trainer.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = trainerCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: Trainer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainerToUpdate = db.Trainers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.TrainerID == id)
                .Single();

            if (TryUpdateModel(trainerToUpdate, "", new string[] { "FirstName", "LastName", "DateOfBirth", "Subject", "OfficeAssignment"}))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(trainerToUpdate.OfficeAssignment.Location))
                    {
                        trainerToUpdate.OfficeAssignment = null;
                    }

                    UpdateTrainerCourses(selectedCourses, trainerToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseData(trainerToUpdate);
            return View(trainerToUpdate);
        }
        private void UpdateTrainerCourses(string[] selectedCourses,Trainer trainerToUpdate)
        {
            if(selectedCourses == null)
            {
                trainerToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var trainerCourses = new HashSet<int>
                (trainerToUpdate.Courses.Select(c => c.CourseID));
           
            foreach(var course in db.Courses)
            {
                if(selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!trainerCourses.Contains(course.CourseID))
                    {
                        trainerToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (trainerCourses.Contains(course.CourseID))
                    {
                        trainerToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        // GET: Trainer/Delete/5
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
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainer trainer = db.Trainers
            .Include(i => i.OfficeAssignment)
            .Where(i => i.TrainerID == id)
            .Single();

            db.Trainers.Remove(trainer);

            var department = db.Departments
               .Where(d => d.TrainerID == id)
               .SingleOrDefault();
            if (department != null)
            {
                department.TrainerID = null;
            }

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
