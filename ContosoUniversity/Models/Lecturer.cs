using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Lecturer 
    {
        public int ID { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        
        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}