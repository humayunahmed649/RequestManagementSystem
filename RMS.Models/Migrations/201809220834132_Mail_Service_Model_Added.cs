namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mail_Service_Model_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        To = c.String(),
                        From = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        MailSendingDateTime = c.DateTime(nullable: false),
                        RequisitionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MailServices", "RequisitionId", "dbo.Requisitions");
            DropIndex("dbo.MailServices", new[] { "RequisitionId" });
            DropTable("dbo.MailServices");
        }
    }
}
