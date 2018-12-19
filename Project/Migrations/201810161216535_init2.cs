namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workshop_Details",
                c => new
                    {
                        Workshop_DetailsId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Begin_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false),
                        Description = c.String(),
                        Audience_Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Workshop_DetailsId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workshop_Details", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Workshop_Details", new[] { "EmployeeId" });
            DropTable("dbo.Workshop_Details");
        }
    }
}
