namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notificatin_Model_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        EmployeeId = c.Int(),
                        RequisitionId = c.Int(),
                        NotifyDateTime = c.DateTime(nullable: false),
                        SenderViewStatus = c.String(),
                        ControllerViewStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId)
                .Index(t => t.EmployeeId)
                .Index(t => t.RequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "RequisitionId", "dbo.Requisitions");
            DropForeignKey("dbo.Notifications", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Notifications", new[] { "RequisitionId" });
            DropIndex("dbo.Notifications", new[] { "EmployeeId" });
            DropTable("dbo.Notifications");
        }
    }
}
