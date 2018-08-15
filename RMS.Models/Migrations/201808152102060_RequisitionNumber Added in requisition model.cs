namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionNumberAddedinrequisitionmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "RequisitionNumber", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requisitions", "RequisitionNumber");
        }
    }
}
