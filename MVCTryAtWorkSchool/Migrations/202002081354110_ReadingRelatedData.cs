namespace MVCTryAtWorkSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReadingRelatedData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        AssignmentID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        OralMark = c.Double(nullable: false),
                        TotalMark = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentID);
            
            CreateTable(
                "dbo.EnrollAssignmentCourse",
                c => new
                    {
                        EnrollAssignmentCourseID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        AssignmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnrollAssignmentCourseID)
                .ForeignKey("dbo.Assignment", t => t.AssignmentID, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.AssignmentID);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Stream = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        TrainerID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Trainer", t => t.TrainerID)
                .Index(t => t.TrainerID);
            
            CreateTable(
                "dbo.Trainer",
                c => new
                    {
                        TrainerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.TrainerID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        TrainerID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TrainerID)
                .ForeignKey("dbo.Trainer", t => t.TrainerID)
                .Index(t => t.TrainerID);
            
            CreateTable(
                "dbo.EnrollStudentCourse",
                c => new
                    {
                        EnrollStudentCourseID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollStudentCourseID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        TuitionFees = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.CourseTrainer",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        TrainerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.TrainerID })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Trainer", t => t.TrainerID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.TrainerID);
            Sql("INSERT INTO dbo.Department (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())");
            //  default value for FK points to department created above.
            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1));
        }

        public override void Down()
        {
            DropForeignKey("dbo.CourseTrainer", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.CourseTrainer", "CourseID", "dbo.Course");
            DropForeignKey("dbo.EnrollStudentCourse", "StudentID", "dbo.Student");
            DropForeignKey("dbo.EnrollStudentCourse", "CourseID", "dbo.Course");
            DropForeignKey("dbo.EnrollAssignmentCourse", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.OfficeAssignment", "TrainerID", "dbo.Trainer");
            DropForeignKey("dbo.EnrollAssignmentCourse", "AssignmentID", "dbo.Assignment");
            DropIndex("dbo.CourseTrainer", new[] { "TrainerID" });
            DropIndex("dbo.CourseTrainer", new[] { "CourseID" });
            DropIndex("dbo.EnrollStudentCourse", new[] { "StudentID" });
            DropIndex("dbo.EnrollStudentCourse", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignment", new[] { "TrainerID" });
            DropIndex("dbo.Department", new[] { "TrainerID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropIndex("dbo.EnrollAssignmentCourse", new[] { "AssignmentID" });
            DropIndex("dbo.EnrollAssignmentCourse", new[] { "CourseID" });
            DropTable("dbo.CourseTrainer");
            DropTable("dbo.Student");
            DropTable("dbo.EnrollStudentCourse");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Trainer");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
            DropTable("dbo.EnrollAssignmentCourse");
            DropTable("dbo.Assignment");
        }
    }
}
