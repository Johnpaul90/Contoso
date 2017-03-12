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
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Departments
        public async Task<ActionResult> Index()
        {
            ViewBag.StudentCount = db.Students.Count();
            ViewBag.DepartmentCount = db.Departments.Count();
            ViewBag.CourseCount = db.Courses.Count();
            ViewBag.LecturerCount = db.Lecturers.Count();

            var departments = db.Departments.Include(d => d.HeadOfDept);
            return View(await departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Department department = await db.Departments.FindAsync(id);

            string query = "SELECT * from Department WHERE DepartmentID =@p0";
            Department department = await db.Departments.SqlQuery(query, id).SingleOrDefaultAsync();
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.LecturerID = new SelectList(db.Lecturers, "ID", "FullName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,LecturerID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
               await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LecturerID = new SelectList(db.Lecturers, "ID", "FullName", department.LecturerID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.LecturerID = new SelectList(db.Lecturers, "ID", "FullName", department.LecturerID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentID, Name, Budget, StartDate, RowVersion,LecturerID")]Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ValidateOneHODAssignmentPerLecturer(department);
                }
                if (ModelState.IsValid)
                {
                    db.Entry(department).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Department)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");
                }
                else
                {
                    var databaseValues = (Department)databaseEntry.ToObject();
                    if (databaseValues.Name != clientValues.Name)
                        ModelState.AddModelError("Name", "Current value: "+ databaseValues.Name);
                    if (databaseValues.Budget != clientValues.Budget)
                        ModelState.AddModelError("Budget", "Current value: "+ String.Format("{0:c}", databaseValues.Budget));
                    if (databaseValues.StartDate != clientValues.StartDate)
                        ModelState.AddModelError("StartDate", "Current value: "+ String.Format("{0:d}", databaseValues.StartDate));
                    if (databaseValues.LecturerID != clientValues.LecturerID)
                        ModelState.AddModelError("LecturerID", "Current value: "+ db.Lecturers.Find(databaseValues.LecturerID).FullName);
                    ModelState.AddModelError(string.Empty, "The record you attemptedto edit " 
                        + "was modified by another user after you got the original value.The "
                        + "edit operation was canceled and the current values in the database "
                        + "have been displayed. If you still want to edit this record, click "
                        + "the Save button again. Otherwise click the Back to List hyperlink.");
                    department.RowVersion = databaseValues.RowVersion;
                }
            }
            catch (RetryLimitExceededException e)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator." + e.Message);
            }
            ViewBag.InstructorID = new SelectList(db.Lecturers, "ID", "FullName",
            department.LecturerID);
            return View(department);
        }

        private void ValidateOneHODAssignmentPerLecturer(Department department)
        {
            if (department.LecturerID != null)
            {
                Department duplicateDepartment = db.Departments
                                                        .Include("HeadOfDept")
                                                        .Where(d => d.LecturerID == department.LecturerID)
                                                        .AsNoTracking()
                                                        .FirstOrDefault();

                if (duplicateDepartment != null && duplicateDepartment.DepartmentID != department.DepartmentID)
                {
                    string errorMessage = String.Format(
                                                        "Instructor {0} {1} is already administrator of the {2} department.",
                                                        duplicateDepartment.HeadOfDept.FirstName,
                                                        duplicateDepartment.HeadOfDept.LastName,
                                                        duplicateDepartment.Name);

                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
