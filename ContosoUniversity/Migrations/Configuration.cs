namespace ContosoUniversity.Migrations
{
    using DAL;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContosoUniversity.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ContosoUniversity.DAL.SchoolContext";
        }

        protected override void Seed(ContosoUniversity.DAL.SchoolContext context)
        {
            /*
            var students = new List<Student>
            {
                new Student{FirstName="Emeka",LastName="Alexander", EnrollmentDate = DateTime.Parse("2016-09-01")},
                new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate = DateTime.Parse("2016-09-01")},
                new Student{FirstName="Arturo",LastName="Vidal",EnrollmentDate = DateTime.Parse("2016-09-01")},
                new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstName="Peggy",LastName="Dada",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2013-09-01")},
                new Student{FirstName="Adeyemi",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2015-09-01")}
            };
            students.ForEach(s => context.Students.AddOrUpdate(p =>p.LastName, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{CourseID=1050,Title="Numerical Analysis",Credits=3,},
                new Course{CourseID=4022,Title="Continuum Mechanics",Credits=3,},
                new Course{CourseID=4041,Title="Complex Analysis",Credits=3,},
                new Course{CourseID=1045,Title="Differential Equations",Credits=4,},
                new Course{CourseID=3141,Title="Real Analysis and Topology",Credits=4,},
                new Course{CourseID=2021,Title="Algebra",Credits=3,},
                new Course{CourseID=2042,Title="Fluid Mechanics",Credits=4,},
                new Course{CourseID=2089,Title="Advanced Calculus",Credits=3,}
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p=>p.Title, s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alexander").ID,
                    CourseID = courses.Single(c => c.Title =="Chemistry" ).CourseID,Grade = Grade.A
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alexander").ID,
                    CourseID = courses.Single(c => c.Title =="Microeconomics" ).CourseID, Grade = Grade.C
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alexander").ID,
                    CourseID = courses.Single(c => c.Title =="Macroeconomics" ).CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alonso").ID,
                    CourseID = courses.Single(c => c.Title =="Calculus" ).CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alonso").ID,
                    CourseID = courses.Single(c => c.Title =="Trigonometry" ).CourseID,Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alonso").ID,
                    CourseID = courses.Single(c => c.Title =="Composition" ).CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Anand").ID,
                    CourseID = courses.Single(c => c.Title =="Chemistry" ).CourseID
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Anand").ID,
                    CourseID = courses.Single(c => c.Title =="Microeconomics").CourseID,Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title =="Chemistry").CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Li").ID,
                    CourseID = courses.Single(c => c.Title =="Composition").CourseID,Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Justice").ID,
                    CourseID = courses.Single(c => c.Title =="Literature").CourseID,Grade = Grade.B
                               }
                   };
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(s =>s.Student.ID == e.StudentID 
                                           && s.Course.CourseID == e.CourseID).SingleOrDefault();

                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
            */


            
            var students = new List<Student>
            {
                new Student { FirstName = "Carson", LastName ="Alexander",EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Arturo", LastName = "Anand",EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Gytis", LastName ="Barzdukas",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Yan", LastName = "Li",EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Peggy", LastName ="Justice",EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Laura", LastName = "Norman",EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Nino", LastName ="Olivetto",EnrollmentDate = DateTime.Parse("2005-09-01") }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();


            var instructors = new List<Lecturer>
            {
                new Lecturer { FirstName = "Gbenga", LastName = "Ogundare", HireDate = DateTime.Parse("1999-02-11")},
                new Lecturer { FirstName = "Oluyemi", LastName = "Layeni", HireDate = DateTime.Parse("1997-05-21")},
                new Lecturer { FirstName = "Obafemi", LastName = "Akinbo", HireDate = DateTime.Parse("2000-02-05")},
                new Lecturer { FirstName = "Akuma", LastName = "Okeke", HireDate = DateTime.Parse("2005-09-17")},
                new Lecturer { FirstName = "Felicity", LastName = "Spoke", HireDate = DateTime.Parse("1999-02-11")},
                new Lecturer { FirstName = "Kim", LastName ="Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
                new Lecturer { FirstName = "Fadi", LastName ="Fakhouri", HireDate = DateTime.Parse("2002-07-06") },
                new Lecturer { FirstName = "Roger", LastName ="Harui",HireDate = DateTime.Parse("1998-07-01") },
                new Lecturer { FirstName = "Candace", LastName ="Kapoor",HireDate = DateTime.Parse("2001-01-15") },
                new Lecturer { FirstName = "Roger", LastName ="Zheng",HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Lecturers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "English Language", Budget = 350000,StartDate = DateTime.Parse("2007-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,StartDate = DateTime.Parse("2007-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Harui").ID },
                new Department { Name = "Economics", Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Kapoor").ID },
                 new Department { Name = "Zoology", Budget = 730000,StartDate = DateTime.Parse("2004-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Okeke").ID },
                 new Department { Name = "Computer Technology", Budget = 2770000,StartDate = DateTime.Parse("1998-09-14"),
                    LecturerID = instructors.Single( i => i.LastName =="Spoke").ID },
                 new Department { Name = "Electrical Electronics", Budget = 2790000,StartDate = DateTime.Parse("2000-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Okeke").ID },
                 new Department { Name = "Physics", Budget = 730000,StartDate = DateTime.Parse("1991-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Zheng").ID },
                 new Department { Name = "Law", Budget = 3000000,StartDate = DateTime.Parse("1992-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Okeke").ID },
                 new Department { Name = "Psychology", Budget = 730000,StartDate = DateTime.Parse("2004-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Okeke").ID },
                 new Department { Name = "Biological Science", Budget = 730000,StartDate = DateTime.Parse("2000-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Okeke").ID },
                 new Department { Name = "Medcine", Budget = 5000000,StartDate = DateTime.Parse("2004-09-01"),
                    LecturerID = instructors.Single( i => i.LastName =="Okeke").ID }

            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();


            var courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",Credits = 3,
                    DepartmentID = departments.Single( s => s.Name =="Engineering").DepartmentID,Lecturers = new List<Lecturer>()},
                new Course {CourseID = 4022, Title = "Microeconomics",Credits = 3,
                    DepartmentID = departments.Single( s => s.Name =="Economics").DepartmentID,Lecturers = new List<Lecturer>()},
                new Course {CourseID = 4041, Title = "Macroeconomics",Credits = 3,
                    DepartmentID = departments.Single( s => s.Name =="Economics").DepartmentID,Lecturers = new List<Lecturer>()},
                new Course {CourseID = 1045, Title = "Calculus",Credits = 4,
                    DepartmentID = departments.Single( s => s.Name =="Mathematics").DepartmentID,Lecturers = new List<Lecturer>()},
                new Course {CourseID = 3141, Title = "Trigonometry",Credits = 4,
                    DepartmentID = departments.Single( s => s.Name =="Mathematics").DepartmentID,Lecturers = new List<Lecturer>()},
                new Course {CourseID = 2021, Title = "Composition",Credits = 3,
                    DepartmentID = departments.Single( s => s.Name =="English").DepartmentID,Lecturers = new List<Lecturer>()},
                new Course {CourseID = 2042, Title = "Literature",Credits = 4,
                    DepartmentID = departments.Single( s => s.Name =="English").DepartmentID,Lecturers = new List<Lecturer>()},
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID,
            s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {LecturerID = instructors.Single( i => i.LastName =="Fakhouri").ID,Location = "Smith 17" },
                new OfficeAssignment {LecturerID = instructors.Single( i => i.LastName =="Harui").ID,Location = "Gowan 27" },
                new OfficeAssignment {LecturerID = instructors.Single( i => i.LastName =="Kapoor").ID,Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.LecturerID, s));
            context.SaveChanges();

            AddOrUpdateLecturer(context, "Chemistry", "Kapoor");
            AddOrUpdateLecturer(context, "Chemistry", "Harui");
            AddOrUpdateLecturer(context, "Microeconomics", "Zheng");
            AddOrUpdateLecturer(context, "Macroeconomics", "Zheng");
            AddOrUpdateLecturer(context, "Calculus", "Fakhouri");
            AddOrUpdateLecturer(context, "Trigonometry", "Harui");
            AddOrUpdateLecturer(context, "Composition", "Abercrombie");
            AddOrUpdateLecturer(context, "Literature", "Abercrombie");
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alexander").ID,
                    CourseID = courses.Single(c => c.Title =="Chemistry" ).CourseID,Grade = Grade.A
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alexander").ID,
                    CourseID = courses.Single(c => c.Title =="Microeconomics" ).CourseID, Grade = Grade.C
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alexander").ID,
                    CourseID = courses.Single(c => c.Title =="Macroeconomics" ).CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alonso").ID,
                    CourseID = courses.Single(c => c.Title =="Calculus" ).CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alonso").ID,
                    CourseID = courses.Single(c => c.Title =="Trigonometry" ).CourseID,Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Alonso").ID,
                    CourseID = courses.Single(c => c.Title =="Composition" ).CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Anand").ID,
                    CourseID = courses.Single(c => c.Title =="Chemistry" ).CourseID
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Anand").ID,
                    CourseID = courses.Single(c => c.Title =="Microeconomics").CourseID,Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title =="Chemistry").CourseID, Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Li").ID,
                    CourseID = courses.Single(c => c.Title =="Composition").CourseID,Grade = Grade.B
                               },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName =="Justice").ID,
                    CourseID = courses.Single(c => c.Title =="Literature").CourseID,Grade = Grade.B
                               }
                   };
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(s => s.Student.ID == e.StudentID
                                           && s.Course.CourseID == e.CourseID).SingleOrDefault();

                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();


        }

        void AddOrUpdateLecturer(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Lecturers.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Lecturers.Add(context.Lecturers.Single(i => i.LastName == instructorName));

        }
        
        }
}
