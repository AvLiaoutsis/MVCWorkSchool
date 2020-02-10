using MVCTryAtWorkSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTryAtWorkSchool.ViewModels
{
    public class TrainerIndexData
    {
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<EnrollStudentCourse> enrollStudentCourses { get; set; }
    }
}