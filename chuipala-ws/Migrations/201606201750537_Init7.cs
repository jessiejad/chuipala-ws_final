namespace chuipala_ws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupProfessors",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        ProfessorID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupID, t.ProfessorID })
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Professor", t => t.ProfessorID)
                .Index(t => t.GroupID)
                .Index(t => t.ProfessorID);
            
            CreateTable(
                "dbo.GroupHomeworks",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        HomeworkID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.HomeworkID })
                .ForeignKey("dbo.Group", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Homework", t => t.HomeworkID, cascadeDelete: true)
                .Index(t => t.GroupID)
                .Index(t => t.HomeworkID);
            
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        HomeworkID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Delivery = c.DateTime(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        ProfessorID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HomeworkID)
                .ForeignKey("dbo.Professor", t => t.ProfessorID)
                .Index(t => t.ProfessorID);
            
            CreateTable(
                "dbo.Tip",
                c => new
                    {
                        TipID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        HomeworkID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipID)
                .ForeignKey("dbo.Homework", t => t.HomeworkID, cascadeDelete: true)
                .Index(t => t.HomeworkID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupProfessors", "ProfessorID", "dbo.Professor");
            DropForeignKey("dbo.GroupProfessors", "GroupID", "dbo.Group");
            DropForeignKey("dbo.Tip", "HomeworkID", "dbo.Homework");
            DropForeignKey("dbo.Homework", "ProfessorID", "dbo.Professor");
            DropForeignKey("dbo.GroupHomeworks", "HomeworkID", "dbo.Homework");
            DropForeignKey("dbo.GroupHomeworks", "GroupID", "dbo.Group");
            DropIndex("dbo.Tip", new[] { "HomeworkID" });
            DropIndex("dbo.Homework", new[] { "ProfessorID" });
            DropIndex("dbo.GroupHomeworks", new[] { "HomeworkID" });
            DropIndex("dbo.GroupHomeworks", new[] { "GroupID" });
            DropIndex("dbo.GroupProfessors", new[] { "ProfessorID" });
            DropIndex("dbo.GroupProfessors", new[] { "GroupID" });
            DropTable("dbo.Tip");
            DropTable("dbo.Homework");
            DropTable("dbo.GroupHomeworks");
            DropTable("dbo.GroupProfessors");
        }
    }
}
