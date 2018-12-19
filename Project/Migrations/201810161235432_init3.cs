namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Training_Details",
                c => new
                    {
                        Training_DetailsId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Begin_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Training_DetailsId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Training_Details", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Training_Details", new[] { "EmployeeId" });
            DropTable("dbo.Training_Details");
        }
    }
}
