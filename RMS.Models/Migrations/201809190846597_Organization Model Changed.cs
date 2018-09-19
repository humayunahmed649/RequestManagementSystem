namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationModelChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Organizations", "Code");
            DropColumn("dbo.Organizations", "RegNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organizations", "RegNo", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Organizations", "Code", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
