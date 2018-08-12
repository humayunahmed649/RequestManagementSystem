namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_required_in_Employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PermanentAddress", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Employees", "PresentAddress", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Employees", "EmployeeTypes", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Employees", "HouseNo");
            DropColumn("dbo.Employees", "RoadNo");
            DropColumn("dbo.Employees", "FloorNo");
            DropColumn("dbo.Employees", "PostOffice");
            DropColumn("dbo.Employees", "District");
            DropColumn("dbo.Employees", "Division");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "EmployeeTypes", c => c.String());
            DropColumn("dbo.Employees", "PresentAddress");
            DropColumn("dbo.Employees", "PermanentAddress");
            AddColumn("dbo.Employees", "HouseNo",c=>c.String());
            AddColumn("dbo.Employees", "RoadNo",c=>c.String());
            AddColumn("dbo.Employees", "FloorNo",c=>c.String());
            AddColumn("dbo.Employees", "PostOffice",c=>c.String());
            AddColumn("dbo.Employees", "District",c=>c.String());
            AddColumn("dbo.Employees", "Division",c=>c.String());
        }
    }
}
