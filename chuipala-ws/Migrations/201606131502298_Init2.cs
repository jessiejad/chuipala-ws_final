namespace chuipala_ws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Professor_Id = c.String(maxLength: 128),
                        Subject_SubjectID = c.Int(),
                    })
                .PrimaryKey(t => t.ClassID)
                .ForeignKey("dbo.Professor", t => t.Professor_Id)
                .ForeignKey("dbo.Subject", t => t.Subject_SubjectID)
                .Index(t => t.Professor_Id)
                .Index(t => t.Subject_SubjectID);
            
            CreateTable(
                "dbo.GroupClasses",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.ClassID })
                .ForeignKey("dbo.Class", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.ClassID);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.GroupStudents",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        StudentID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupID, t.StudentID })
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID)
                .Index(t => t.GroupID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Reference = c.String(),
                    })
                .PrimaryKey(t => t.SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Class", "Subject_SubjectID", "dbo.Subject");
            DropForeignKey("dbo.Class", "Professor_Id", "dbo.Professor");
            DropForeignKey("dbo.GroupStudents", "StudentID", "dbo.Student");
            DropForeignKey("dbo.GroupStudents", "GroupID", "dbo.Group");
            DropForeignKey("dbo.GroupClasses", "GroupID", "dbo.Group");
            DropForeignKey("dbo.GroupClasses", "ClassID", "dbo.Class");
            DropIndex("dbo.GroupStudents", new[] { "StudentID" });
            DropIndex("dbo.GroupStudents", new[] { "GroupID" });
            DropIndex("dbo.GroupClasses", new[] { "ClassID" });
            DropIndex("dbo.GroupClasses", new[] { "GroupID" });
            DropIndex("dbo.Class", new[] { "Subject_SubjectID" });
            DropIndex("dbo.Class", new[] { "Professor_Id" });
            DropTable("dbo.Subject");
            DropTable("dbo.GroupStudents");
            DropTable("dbo.Group");
            DropTable("dbo.GroupClasses");
            DropTable("dbo.Class");
        }
    }
}
