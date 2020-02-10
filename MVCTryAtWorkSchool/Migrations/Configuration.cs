namespace MVCTryAtWorkSchool.Migrations
{
    using MVCTryAtWorkSchool.DAL;
    using MVCTryAtWorkSchool.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCTryAtWorkSchool.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander",
                    DateOfBirth = DateTime.Parse("2010-09-01"), TuitionFees = 500 },
                new Student { FirstName = "Meredith", LastName = "Alonso",
                    DateOfBirth = DateTime.Parse("2012-09-01") , TuitionFees = 600},
                new Student { FirstName = "Arturo",   LastName = "Anand",
                    DateOfBirth = DateTime.Parse("2013-09-01") , TuitionFees = 700},
                new Student { FirstName = "Gytis",    LastName = "Barzdukas",
                    DateOfBirth = DateTime.Parse("2012-09-01"), TuitionFees = 800 },
                new Student { FirstName = "Yan",      LastName = "Li",
                    DateOfBirth = DateTime.Parse("2012-09-01") , TuitionFees = 900},
                new Student { FirstName = "Peggy",    LastName = "Justice",
                    DateOfBirth = DateTime.Parse("2011-09-01") , TuitionFees = 1000},
                new Student { FirstName = "Laura",    LastName = "Norman",
                    DateOfBirth = DateTime.Parse("2013-09-01") , TuitionFees = 10000},
                new Student { FirstName = "Nino",     LastName = "Olivetto",
                    DateOfBirth = DateTime.Parse("2005-08-11"), TuitionFees = 20000 }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();


            var trainers = new List<Trainer>
            {
                new Trainer { FirstName = "Kim",     LastName = "Abercrombie",
                    DateOfBirth = DateTime.Parse("1995-03-11") },
                new Trainer { FirstName = "Fadi",    LastName = "Fakhouri",
                    DateOfBirth = DateTime.Parse("2002-07-06") },
                new Trainer { FirstName = "Roger",   LastName = "Harui",
                    DateOfBirth = DateTime.Parse("1998-07-01") },
                new Trainer { FirstName = "Candace", LastName = "Kapoor",
                    DateOfBirth = DateTime.Parse("2001-01-15") },
                new Trainer { FirstName = "Roger",   LastName = "Zheng",
                    DateOfBirth = DateTime.Parse("2004-02-12") }
            };
            trainers.ForEach(s => context.Trainers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var assignments = new List<Assignment>
            {
                new Assignment { Title = "1st Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "2nd Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "3rd Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "4th Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "5th Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "6th Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "7th Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "8th Assignment",
                    Description = "Lorem ipsum"},
                new Assignment { Title = "Final Assignment",
                    Description = "Lorem ipsum"}
            };
            assignments.ForEach(s => context.Assignments.AddOrUpdate(p => p.AssignmentID, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TrainerID  = trainers.Single( i => i.LastName == "Abercrombie").TrainerID },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TrainerID  = trainers.Single( i => i.LastName == "Fakhouri").TrainerID },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TrainerID  = trainers.Single( i => i.LastName == "Harui").TrainerID },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    TrainerID  = trainers.Single( i => i.LastName == "Kapoor").TrainerID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry", StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                },
                new Course {CourseID = 4022, Title = "Microeconomics", StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                },
                new Course {CourseID = 1045, Title = "Calculus", StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                },
                new Course {CourseID = 3141, Title = "Trigonometry",StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                },
                new Course {CourseID = 2021, Title = "Composition",StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                },
                new Course {CourseID = 2042, Title = "Literature", StartDate = DateTime.Parse("2005-08-11"),EndDate = DateTime.Parse("2005-08-11"),
                  DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                  Trainers = new List<Trainer>(),Stream = "random",Type = "Random"
                }
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    TrainerID = trainers.Single( i => i.LastName == "Fakhouri").TrainerID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    TrainerID = trainers.Single( i => i.LastName == "Harui").TrainerID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    TrainerID = trainers.Single( i => i.LastName == "Kapoor").TrainerID,
                    Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.TrainerID, s));
            context.SaveChanges();

            AddOrUpdateTrainer(context, "Chemistry", "Kapoor");
            AddOrUpdateTrainer(context, "Chemistry", "Harui");
            AddOrUpdateTrainer(context, "Microeconomics", "Zheng");
            AddOrUpdateTrainer(context, "Macroeconomics", "Zheng");

            AddOrUpdateTrainer(context, "Calculus", "Fakhouri");
            AddOrUpdateTrainer(context, "Trigonometry", "Harui");
            AddOrUpdateTrainer(context, "Composition", "Abercrombie");
            AddOrUpdateTrainer(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var enrollments = new List<EnrollStudentCourse>
            {
                new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Alexander").StudentID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Alexander").StudentID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Alexander").StudentID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new EnrollStudentCourse {
                     StudentID = students.Single(s => s.LastName == "Alonso").StudentID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new EnrollStudentCourse {
                     StudentID = students.Single(s => s.LastName == "Alonso").StudentID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Alonso").StudentID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Anand").StudentID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Anand").StudentID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").StudentID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Li").StudentID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new EnrollStudentCourse {
                    StudentID = students.Single(s => s.LastName == "Justice").StudentID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };

            foreach (EnrollStudentCourse e in enrollments)
            {
                var enrollmentInDataBase = context.EnrollStudentCourses.Where(
                    s =>
                         s.Student.StudentID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.EnrollStudentCourses.Add(e);
                }
            }
            context.SaveChanges();


        
        }
        void AddOrUpdateTrainer(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Trainers.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Trainers.Add(context.Trainers.Single(i => i.LastName == instructorName));
        }
    }
}
