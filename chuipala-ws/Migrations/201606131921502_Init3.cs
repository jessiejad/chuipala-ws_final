namespace chuipala_ws.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Class", "Subject_SubjectID", "dbo.Subject");
            DropIndex("dbo.Class", new[] { "Subject_SubjectID" });
            RenameColumn(table: "dbo.Class", name: "Professor_Id", newName: "ProfessorID");
            RenameColumn(table: "dbo.Class", name: "Subject_SubjectID", newName: "SubjectID");
            RenameIndex(table: "dbo.Class", name: "IX_Professor_Id", newName: "IX_ProfessorID");
            AlterColumn("dbo.Class", "SubjectID", c => c.Int(nullable: false));
            CreateIndex("dbo.Class", "SubjectID");
            AddForeignKey("dbo.Class", "SubjectID", "dbo.Subject", "SubjectID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Class", "SubjectID", "dbo.Subject");
            DropIndex("dbo.Class", new[] { "SubjectID" });
            AlterColumn("dbo.Class", "SubjectID", c => c.Int());
            RenameIndex(table: "dbo.Class", name: "IX_ProfessorID", newName: "IX_Professor_Id");
            RenameColumn(table: "dbo.Class", name: "SubjectID", newName: "Subject_SubjectID");
            RenameColumn(table: "dbo.Class", name: "ProfessorID", newName: "Professor_Id");
            CreateIndex("dbo.Class", "Subject_SubjectID");
            AddForeignKey("dbo.Class", "Subject_SubjectID", "dbo.Subject", "SubjectID");
        }
    }
}
