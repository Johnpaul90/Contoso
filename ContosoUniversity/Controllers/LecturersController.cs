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
using ContosoUniversity.ViewModels;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
    public class LecturersController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Lecturers
        public ActionResult Index(int? id, int? courseID)
        {
            ViewBag.StudentCount = db.Students.Count();
            ViewBag.DepartmentCount = db.Departments.Count();
            ViewBag.CourseCount = db.Courses.Count();
            ViewBag.LecturerCount = db.Lecturers.Count();

            var viewModel = new LecturerIndexData();

            viewModel.Lecturers = db.Lecturers
                .Include(l => l.OfficeAssignment)
                .Include(l => l.Courses.Select(c => c.Department))
                .OrderBy(l => l.LastName);

            if (id != null)
            {
                ViewBag.LecturerID = id.Value;
                ViewBag.Courses = viewModel.Lecturers.Where(i => i.ID == id.Value).Single().Courses;
            }

            if (courseID  != null)
            {
                ViewBag.CourseID = courseID.Value;
                ViewBag.Enrollments = viewModel.Courses.Where(x => x.CourseID == courseID.Value).Single().Enrollments;

            }
            return View(viewModel);
        }

        // GET: Lecturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // GET: Lecturers/Create
        public ActionResult Create()
        {
            var lecturer = new Lecturer();
            lecturer.Courses = new List<Course>();
            populateAssignedCourseData(lecturer);

            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,HireDate,OfficeAssignment")] Lecturer lecturer, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                lecturer.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    lecturer.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Lecturers.Add(lecturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            populateAssignedCourseData(lecturer);
            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.ID == id)
                .Single();
            populateAssignedCourseData(lecturer);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }
        private void populateAssignedCourseData(Lecturer lecturer)
        {
            var allCourses = db.Courses;
            var lecturerCourses = new HashSet<int>(lecturer.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();

            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned =lecturerCourses.Contains(course.CourseID)
                    
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lecturerToUpdate = db.Lecturers
                                    .Include(i => i.OfficeAssignment)
                                    .Include(i => i.Courses)
                                    .Where(i => i.ID == id)
                                    .Single();
            if (TryUpdateModel(lecturerToUpdate, "",new string[] { "LastName", "FirstMidName", "HireDate","OfficeAssignment" }))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(lecturerToUpdate.OfficeAssignment.Location))
                    {
                        lecturerToUpdate.OfficeAssignment = null;
                    }
                    updateLecturerCourses(selectedCourses, lecturerToUpdate);
                    db.Entry(lecturerToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            populateAssignedCourseData(lecturerToUpdate);
            return View(lecturerToUpdate);

        }

        private void updateLecturerCourses(string[] selectedCourses, Lecturer lecturerToUpdate)
        {
            if (selectedCourses == null)
            {
                lecturerToUpdate.Courses = new List<Course>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var lecturerCourses = new HashSet<int>(lecturerToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!lecturerCourses.Contains(course.CourseID))
                    {
                        lecturerToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (lecturerCourses.Contains(course.CourseID))
                    {
                        lecturerToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
        // GET: Lecturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecturer lecturer = db.Lecturers.Find(id);
            if (lecturer == null)
            {
                return HttpNotFound();
            }
            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lecturer lecturer = db.Lecturers
                .Include(i => i.OfficeAssignment)
                .Where(i => i.ID == id)
                .Single();

            lecturer.OfficeAssignment = null;
            db.Lecturers.Remove(lecturer);

            var department = db.Departments
                .Where(d => d.LecturerID == id)
                .SingleOrDefault();

            if (department != null)
            {
                department.LecturerID = null;
            }
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
