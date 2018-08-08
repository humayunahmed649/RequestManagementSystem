namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requisition_validation_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requisitions", "FromPlace", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Requisitions", "DestinationPlace", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Requisitions", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Requisitions", "RequestFor", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requisitions", "RequestFor", c => c.String());
            AlterColumn("dbo.Requisitions", "Description", c => c.String());
            AlterColumn("dbo.Requisitions", "DestinationPlace", c => c.String());
            AlterColumn("dbo.Requisitions", "FromPlace", c => c.String());
        }
    }
}
