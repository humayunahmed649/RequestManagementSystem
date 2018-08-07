namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Organization_Validation_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Organizations", "Code", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Organizations", "RegNo", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizations", "RegNo", c => c.String());
            AlterColumn("dbo.Organizations", "Code", c => c.String());
            AlterColumn("dbo.Organizations", "Name", c => c.String());
        }
    }
}
