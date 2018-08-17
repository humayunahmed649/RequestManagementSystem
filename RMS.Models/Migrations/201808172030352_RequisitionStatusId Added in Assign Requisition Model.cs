namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionStatusIdAddedinAssignRequisitionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignRequisitions", "RequisitionStatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignRequisitions", "RequisitionStatusId");
            AddForeignKey("dbo.AssignRequisitions", "RequisitionStatusId", "dbo.RequisitionStatus", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignRequisitions", "RequisitionStatusId", "dbo.RequisitionStatus");
            DropIndex("dbo.AssignRequisitions", new[] { "RequisitionStatusId" });
            DropColumn("dbo.AssignRequisitions", "RequisitionStatusId");
        }
    }
}
