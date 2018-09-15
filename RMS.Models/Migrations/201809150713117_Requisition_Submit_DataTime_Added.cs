namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requisition_Submit_DataTime_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "SubmitDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requisitions", "SubmitDateTime");
        }
    }
}
