namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionNumberAddedinAssignRequisitionModelandRequisitionStatusModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignRequisitions", "RequisitionNumber", c => c.String());
            AddColumn("dbo.RequisitionStatus", "RequisitionNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionStatus", "RequisitionNumber");
            DropColumn("dbo.AssignRequisitions", "RequisitionNumber");
        }
    }
}
