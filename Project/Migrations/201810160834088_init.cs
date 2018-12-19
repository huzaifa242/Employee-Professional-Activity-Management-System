namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auths", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Auths", "Auth_Role", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Dob", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "Current_Addr", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Contact_No", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String());
            AlterColumn("dbo.Employees", "Contact_No", c => c.String());
            AlterColumn("dbo.Employees", "Gender", c => c.String());
            AlterColumn("dbo.Employees", "Current_Addr", c => c.String());
            AlterColumn("dbo.Employees", "Dob", c => c.DateTime());
            AlterColumn("dbo.Employees", "Name", c => c.String());
            AlterColumn("dbo.Auths", "Auth_Role", c => c.String());
            AlterColumn("dbo.Auths", "password", c => c.String());
        }
    }
}
