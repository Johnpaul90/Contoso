using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
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
            students.ForEach(s => context.Students.Add(s));
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
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.E},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=6,CourseID=2089,Grade=Grade.C},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050,},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.E},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
                new Enrollment{StudentID=6,CourseID=1045,Grade=Grade.A}
           };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    
    }
}