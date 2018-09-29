namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CancelRequisitionModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CancelRequisitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(nullable: false),
                        RequisitionStatusId = c.Int(nullable: false),
                        RequisitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId, cascadeDelete: true)
                .ForeignKey("dbo.RequisitionStatus", t => t.RequisitionStatusId, cascadeDelete: true)
                .Index(t => t.RequisitionStatusId)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CancelRequisitions", "RequisitionStatusId", "dbo.RequisitionStatus");
            DropForeignKey("dbo.CancelRequisitions", "RequisitionId", "dbo.Requisitions");
            DropIndex("dbo.CancelRequisitions", new[] { "RequisitionId" });
            DropIndex("dbo.CancelRequisitions", new[] { "RequisitionStatusId" });
            DropTable("dbo.CancelRequisitions");
        }
    }
}
