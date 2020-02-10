using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCTryAtWorkSchool.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [Required]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Title name cannot be longer than 50 characters.")]
        public string Title { get; set; }
        
        [Required]
        public string Stream { get; set; }

        [Required]
        public string Type { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
        
        [Display(Name = "Assignments")]
        public virtual ICollection<EnrollAssignmentCourse> EnrollAssignmentCourses { get; set; }
        public virtual ICollection<EnrollStudentCourse> EnrollStudentCourses { get; set; }
        public virtual ICollection<Trainer> Trainers { get; set; }

    }
}