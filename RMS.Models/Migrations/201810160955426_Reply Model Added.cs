namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplyModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReplyText = c.String(nullable: false, maxLength: 250),
                        FeedbackId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Feedbacks", t => t.FeedbackId, cascadeDelete: false)
                .Index(t => t.FeedbackId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "FeedbackId", "dbo.Feedbacks");
            DropForeignKey("dbo.Replies", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Replies", new[] { "EmployeeId" });
            DropIndex("dbo.Replies", new[] { "FeedbackId" });
            DropTable("dbo.Replies");
        }
    }
}
