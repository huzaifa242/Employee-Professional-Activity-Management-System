namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auths",
                c => new
                    {
                        AuthId = c.Int(nullable: false),
                        password = c.String(),
                        Auth_Role = c.String(),
                    })
                .PrimaryKey(t => t.AuthId)
                .ForeignKey("dbo.Employees", t => t.AuthId)
                .Index(t => t.AuthId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Dob = c.DateTime(),
                        Permanent_Addr = c.String(),
                        Current_Addr = c.String(),
                        Marital_Status = c.String(),
                        Gender = c.String(),
                        Contact_No = c.String(),
                        Email = c.String(),
                        Blood_Group = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auths", "AuthId", "dbo.Employees");
            DropIndex("dbo.Auths", new[] { "AuthId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Auths");
        }
    }
}
