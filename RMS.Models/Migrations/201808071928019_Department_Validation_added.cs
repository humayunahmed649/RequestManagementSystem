namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Department_Validation_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Departments", "Code", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "Code", c => c.String());
            AlterColumn("dbo.Departments", "Name", c => c.String());
        }
    }
}
