using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Lecturer")]
        public int LecturerID { get; set; }

        [StringLength(50)]
        [Display(Name ="Office Location")]
        public string Location { get; set; }

        public virtual Lecturer Lecturer { get; set; }
    }
}