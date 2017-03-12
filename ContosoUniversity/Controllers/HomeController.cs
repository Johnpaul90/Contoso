using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            ViewBag.StudentCount = db.Students.Count();
            ViewBag.DepartmentCount = db.Departments.Count();
            ViewBag.CourseCount = db.Courses.Count();
            ViewBag.LecturerCount = db.Lecturers.Count();
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dataGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dataGroup.Key,
                                                       StudentCount = dataGroup.Count()
                                                   };

            return View(data.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}