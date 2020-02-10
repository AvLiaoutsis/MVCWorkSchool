using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTryAtWorkSchool.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Trainer")]
        public int TrainerID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }
        
        public virtual Trainer Trainer { get; set; }
    }
}