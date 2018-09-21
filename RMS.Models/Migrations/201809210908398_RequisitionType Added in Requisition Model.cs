namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionTypeAddedinRequisitionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "RequisitionType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requisitions", "RequisitionType");
        }
    }
}
