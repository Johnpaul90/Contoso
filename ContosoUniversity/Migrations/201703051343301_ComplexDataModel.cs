namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        LecturerID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Lecturer", t => t.LecturerID)
                .Index(t => t.LecturerID);
            
            CreateTable(
                "dbo.Lecturer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        LecturerID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.LecturerID)
                .ForeignKey("dbo.Lecturer", t => t.LecturerID)
                .Index(t => t.LecturerID);
            
            CreateTable(
                "dbo.CourseLecturer",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        LecturerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.LecturerID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Lecturer", t => t.LecturerID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.LecturerID);

            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));
            Sql("INSERT INTO dbo.Department (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())");
            AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Course", "DepartmentID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseLecturer", "LecturerID", "dbo.Lecturer");
            DropForeignKey("dbo.CourseLecturer", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Department", "LecturerID", "dbo.Lecturer");
            DropForeignKey("dbo.OfficeAssignment", "LecturerID", "dbo.Lecturer");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropIndex("dbo.CourseLecturer", new[] { "LecturerID" });
            DropIndex("dbo.CourseLecturer", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignment", new[] { "LecturerID" });
            DropIndex("dbo.Department", new[] { "LecturerID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropColumn("dbo.Course", "DepartmentID");
            DropTable("dbo.CourseLecturer");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Lecturer");
            DropTable("dbo.Department");
        }
    }
}
