namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackCommentAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(nullable: false, maxLength: 250),
                        RequisitionId = c.Int(nullable: false),
                        FeedBackId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId, cascadeDelete: true)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "RequisitionId", "dbo.Requisitions");
            DropIndex("dbo.Feedbacks", new[] { "RequisitionId" });
            DropTable("dbo.Feedbacks");
        }
    }
}
