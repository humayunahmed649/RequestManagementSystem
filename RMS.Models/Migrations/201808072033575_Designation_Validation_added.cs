namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Designation_Validation_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Designations", "Title", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Designations", "Title", c => c.String());
        }
    }
}
