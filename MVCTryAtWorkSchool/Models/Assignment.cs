using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTryAtWorkSchool.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters.")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Oral Mark")]
        public Double OralMark { get; set; }
        [Display(Name = "Total Mark")]
        public Double TotalMark { get; set; }


        public virtual ICollection<EnrollAssignmentCourse> EnrollAssignmentCourses { get; set; }
    }
}