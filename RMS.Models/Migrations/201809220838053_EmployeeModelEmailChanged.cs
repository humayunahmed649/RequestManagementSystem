namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeModelEmailChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
