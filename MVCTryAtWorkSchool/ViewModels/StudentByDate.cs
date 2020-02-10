using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTryAtWorkSchool.ViewModels
{
    public class StudentByDate
    {
        [DataType(DataType.Date)]
        public DateTime ? BirthDate { get; set; }

        public int StudentCount { get; set; }
    }
}