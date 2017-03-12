using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Course ID")]
        public int CourseID { get; set; }

        [Display(Name ="Course Title")]
        public string Title { get; set; }

        [Display(Name ="Credit Unit")]
        public int Credits { get; set; }

        [Display(Name ="Department")]
        public int DepartmentID { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}