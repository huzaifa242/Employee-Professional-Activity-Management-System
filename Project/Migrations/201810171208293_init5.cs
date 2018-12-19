namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Confirmations",
                c => new
                    {
                        ConfirmationId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        fname = c.String(nullable: false),
                        ftype = c.String(),
                    })
                .PrimaryKey(t => t.ConfirmationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            AddColumn("dbo.Workshop_Details", "fname", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Confirmations", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Confirmations", new[] { "EmployeeId" });
            DropColumn("dbo.Workshop_Details", "fname");
            DropTable("dbo.Confirmations");
        }
    }
}
