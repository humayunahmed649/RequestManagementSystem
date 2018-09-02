namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionStatusModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequisitionStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusType = c.String(),
                        RequisitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId, cascadeDelete: false)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequisitionStatus", "RequisitionId", "dbo.Requisitions");
            DropIndex("dbo.RequisitionStatus", new[] { "RequisitionId" });
            DropTable("dbo.RequisitionStatus");
        }
    }
}
