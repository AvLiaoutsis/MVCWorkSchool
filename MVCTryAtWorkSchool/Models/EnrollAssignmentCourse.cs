using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTryAtWorkSchool.Models
{
    public class EnrollAssignmentCourse
    {
        public int EnrollAssignmentCourseID { get; set; }

        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [Display(Name = "Assignment")]
        public int AssignmentID { get; set; }

        public virtual Course Course { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}