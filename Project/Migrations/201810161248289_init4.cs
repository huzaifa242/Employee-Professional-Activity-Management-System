namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Turnkey_Project",
                c => new
                    {
                        Turnkey_ProjectId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Project_Name = c.String(nullable: false),
                        Begin_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Role = c.String(nullable: false),
                        Description = c.String(),
                        Team_Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Turnkey_ProjectId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Turnkey_Project", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Turnkey_Project", new[] { "EmployeeId" });
            DropTable("dbo.Turnkey_Project");
        }
    }
}
