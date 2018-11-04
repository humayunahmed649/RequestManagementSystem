namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionHistoryModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        SubmitDateTime = c.DateTime(),
                        UpdateDateTime = c.DateTime(),
                        DeletedDateTime = c.DateTime(),
                        RequisitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId, cascadeDelete: false)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionHistories", "RequisitionId", "dbo.Requisitions");
            DropIndex("dbo.RequisitionHistories", new[] { "RequisitionId" });
            DropTable("dbo.RequisitionHistories");
        }
    }
}
