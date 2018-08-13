namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_model_change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "DrivingLicence", c => c.String(maxLength: 25));
            DropColumn("dbo.Employees", "PermanentAddress");
            DropColumn("dbo.Employees", "PresentAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "PresentAddress", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Employees", "PermanentAddress", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.Employees", "DrivingLicence");
        }
    }
}
