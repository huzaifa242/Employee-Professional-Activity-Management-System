namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Educational_details",
                c => new
                    {
                        Educational_detailsId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Course_Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Educational_detailsId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Educational_details", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Educational_details", new[] { "EmployeeId" });
            DropTable("dbo.Educational_details");
        }
    }
}
