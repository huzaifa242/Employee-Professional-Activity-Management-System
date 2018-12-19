namespace Project
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;

    public class ProjectData : DbContext
    {
        // Your context has been configured to use a 'ProjectData' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Project.ProjectData' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProjectData' 
        // connection string in the application configuration file.
        public ProjectData()
            : base("name=ProjectData")
        {
        }
        public DbSet<Project.Models.Employee> employees { get; set; }
        public IEnumerable Employees { get; internal set; }
        public DbSet<Project.Models.Auth> auths { get; set; }

        public System.Data.Entity.DbSet<Project.Models.Educational_details> Educational_details { get; set; }

        public System.Data.Entity.DbSet<Project.Models.Workshop_Details> Workshop_Details { get; set; }

        public System.Data.Entity.DbSet<Project.Models.Training_Details> Training_Details { get; set; }

        public System.Data.Entity.DbSet<Project.Models.Turnkey_Project> Turnkey_Project { get; set; }

        public System.Data.Entity.DbSet<Project.Models.Confirmation> Confirmations { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}