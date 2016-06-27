namespace chuipala_ws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbsenceClasses",
                c => new
                    {
                        AbsenceID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                        Absence_EventID = c.Int(),
                    })
                .PrimaryKey(t => new { t.AbsenceID, t.ClassID })
                .ForeignKey("dbo.Absence", t => t.Absence_EventID)
                .ForeignKey("dbo.Class", t => t.ClassID, cascadeDelete: true)
                .Index(t => t.ClassID)
                .Index(t => t.Absence_EventID);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                        Value = c.Int(nullable: false),
                        ValueUnit = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Absence",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        StopDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Delay",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .ForeignKey("dbo.Class", t => t.ClassID, cascadeDelete: true)
                .Index(t => t.EventID)
                .Index(t => t.ClassID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Delay", "ClassID", "dbo.Class");
            DropForeignKey("dbo.Delay", "EventID", "dbo.Event");
            DropForeignKey("dbo.Absence", "EventID", "dbo.Event");
            DropForeignKey("dbo.Event", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AbsenceClasses", "ClassID", "dbo.Class");
            DropForeignKey("dbo.AbsenceClasses", "Absence_EventID", "dbo.Absence");
            DropIndex("dbo.Delay", new[] { "ClassID" });
            DropIndex("dbo.Delay", new[] { "EventID" });
            DropIndex("dbo.Absence", new[] { "EventID" });
            DropIndex("dbo.Event", new[] { "UserID" });
            DropIndex("dbo.AbsenceClasses", new[] { "Absence_EventID" });
            DropIndex("dbo.AbsenceClasses", new[] { "ClassID" });
            DropTable("dbo.Delay");
            DropTable("dbo.Absence");
            DropTable("dbo.Event");
            DropTable("dbo.AbsenceClasses");
        }
    }
}
