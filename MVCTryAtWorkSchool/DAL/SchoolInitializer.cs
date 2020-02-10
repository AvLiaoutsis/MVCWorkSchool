using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCTryAtWorkSchool.Models;
using System.Windows;

namespace MVCTryAtWorkSchool.DAL
{
    public class SchoolInitializer : DropCreateDatabaseAlways<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
            new Student{FirstName="Carson",LastName="Alexander",DateOfBirth=DateTime.Parse("2005-09-01"),TuitionFees = 500},
            new Student{FirstName="Meredith",LastName="Alonso",DateOfBirth=DateTime.Parse("2002-09-01"),TuitionFees = 5300},
            new Student{FirstName="Arturo",LastName="Anand",DateOfBirth=DateTime.Parse("2003-09-01"),TuitionFees = 3100},
            new Student{FirstName="Gytis",LastName="Barzdukas",DateOfBirth=DateTime.Parse("2002-09-01"),TuitionFees = 5200},
            new Student{FirstName="Yan",LastName="Li",DateOfBirth=DateTime.Parse("2002-09-01")},
            new Student{FirstName="Peggy",LastName="Justice",DateOfBirth=DateTime.Parse("2001-09-01"),TuitionFees = 55600},
            new Student{FirstName="Laura",LastName="Norman",DateOfBirth=DateTime.Parse("2003-09-01"),TuitionFees = 5300},
            new Student{FirstName="Nino",LastName="Olivetto",DateOfBirth=DateTime.Parse("2005-09-01"),TuitionFees = 21500}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var courses = new List<Course>
            {
            new Course{CourseID=1050,Title="Chemistry",Stream="PC",Type="1st",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")},
            new Course{CourseID=4022,Title="Microeconomics",Stream="MAC",Type="2nd",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")},
            new Course{CourseID=4041,Title="Macroeconomics",Stream="Windows",Type="3rd",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")},
            new Course{CourseID=1045,Title="Calculus",Stream="Sapouni",Type="4th",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")},
            new Course{CourseID=3141,Title="Trigonometry",Stream="Algebra",Type="5th",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")},
            new Course{CourseID=2021,Title="Composition",Stream="Maths",Type="6th",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")},
            new Course{CourseID=2042,Title="Literature",Stream="Essay",Type="7th",StartDate = DateTime.Parse("2005-09-01"),EndDate = DateTime.Parse("2010-09-01")}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollStudentCourses = new List<EnrollStudentCourse>
            {
            new EnrollStudentCourse{StudentID=1,CourseID=1050,Grade=Grade.A},
            new EnrollStudentCourse{StudentID=1,CourseID=4022,Grade=Grade.C},
            new EnrollStudentCourse{StudentID=1,CourseID=4041,Grade=Grade.B},
            new EnrollStudentCourse{StudentID=2,CourseID=1045,Grade=Grade.B},
            new EnrollStudentCourse{StudentID=2,CourseID=3141,Grade=Grade.F},
            new EnrollStudentCourse{StudentID=2,CourseID=2021,Grade=Grade.F},
            new EnrollStudentCourse{StudentID=3,CourseID=1050},
            new EnrollStudentCourse{StudentID=4,CourseID=1050},
            new EnrollStudentCourse{StudentID=4,CourseID=4022,Grade=Grade.F},
            new EnrollStudentCourse{StudentID=5,CourseID=4041,Grade=Grade.C},
            new EnrollStudentCourse{StudentID=6,CourseID=1045},
            new EnrollStudentCourse{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            enrollStudentCourses.ForEach(s => context.EnrollStudentCourses.Add(s));
            context.SaveChanges();
        }
    }
}