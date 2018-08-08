namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeTypes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmployeeTypes");
        }
    }
}
