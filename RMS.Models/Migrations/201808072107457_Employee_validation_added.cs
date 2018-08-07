namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_validation_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FullName", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "ContactNo", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "NID", c => c.String(nullable: false, maxLength: 19));
            AlterColumn("dbo.Employees", "District", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "District", c => c.String());
            AlterColumn("dbo.Employees", "NID", c => c.String());
            AlterColumn("dbo.Employees", "ContactNo", c => c.String());
            AlterColumn("dbo.Employees", "Email", c => c.String());
            AlterColumn("dbo.Employees", "FullName", c => c.String());
        }
    }
}
