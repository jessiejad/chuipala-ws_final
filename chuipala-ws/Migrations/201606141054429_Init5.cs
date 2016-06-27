namespace chuipala_ws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AbsenceClasses", new[] { "Absence_EventID" });
            RenameColumn(table: "dbo.AbsenceClasses", name: "Absence_EventID", newName: "EventID");
            DropPrimaryKey("dbo.AbsenceClasses");
            AlterColumn("dbo.AbsenceClasses", "EventID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AbsenceClasses", new[] { "EventID", "ClassID" });
            CreateIndex("dbo.AbsenceClasses", "EventID");
            DropColumn("dbo.AbsenceClasses", "AbsenceID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AbsenceClasses", "AbsenceID", c => c.Int(nullable: false));
            DropIndex("dbo.AbsenceClasses", new[] { "EventID" });
            DropPrimaryKey("dbo.AbsenceClasses");
            AlterColumn("dbo.AbsenceClasses", "EventID", c => c.Int());
            AddPrimaryKey("dbo.AbsenceClasses", new[] { "AbsenceID", "ClassID" });
            RenameColumn(table: "dbo.AbsenceClasses", name: "EventID", newName: "Absence_EventID");
            CreateIndex("dbo.AbsenceClasses", "Absence_EventID");
        }
    }
}
