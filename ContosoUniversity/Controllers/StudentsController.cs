using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)        
        {
            ViewBag.StudentCount = db.Students.Count();
            ViewBag.DepartmentCount = db.Departments.Count();
            ViewBag.CourseCount = db.Courses.Count();
            ViewBag.LecturerCount = db.Lecturers.Count();


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString!= null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;

            var students = from s in db.Students
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                           || s.FirstName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;

                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;

                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber,pageSize));
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (RetryLimitExceededException e)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator." +e.Message);
            }
            
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit1(int? id)
        {
            
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }                           
            return PartialView(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Edit([Bind(Include = "ID, LastName, FirstName,EnrollmentDate")]Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator." + e.Message);
            }
            return View(student);

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Edit(object formData)
        //{
        //    var studentForm = HttpContext.Request.Form;
        //    var student = new Student();
        //    student.ID = int.Parse(studentForm[1]);
        //    student.LastName = studentForm[2];
        //    student.FirstName = studentForm[3];
        //    student.EnrollmentDate = DateTime.Parse(studentForm[4]);


        //    var s = db.Students.Find(student.ID);
        //    s.LastName = student.LastName;
        //    s.FirstName = student.FirstName;
        //    s.EnrollmentDate = student.EnrollmentDate;


        //    if (ModelState.IsValid)
        //    {

        //        db.Entry(s).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json("true", JsonRequestBehavior.AllowGet);


        //    }
        //    return Json("false", JsonRequestBehavior.AllowGet);
        //}

        // GET: Students/Delete/5
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return PartialView(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
