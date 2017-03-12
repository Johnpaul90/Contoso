using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Department
    {
        [Display(Name ="Department")]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}",ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? LecturerID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Lecturer HeadOfDept { get; set; }
        public virtual ICollection<Course> Courses { get; set; }



    }
}