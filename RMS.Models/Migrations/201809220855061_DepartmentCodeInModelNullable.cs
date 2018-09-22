namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentCodeInModelNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "Code", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
