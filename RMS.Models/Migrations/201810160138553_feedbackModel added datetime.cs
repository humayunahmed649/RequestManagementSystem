namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackModeladdeddatetime : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Feedbacks", new[] { "EmployeeId" });
            AddColumn("dbo.Feedbacks", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Feedbacks", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "EmployeeId");
            AddForeignKey("dbo.Feedbacks", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: false);
            DropColumn("dbo.Feedbacks", "FeedBackId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "FeedBackId", c => c.Int());
            DropForeignKey("dbo.Feedbacks", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Feedbacks", new[] { "EmployeeId" });
            AlterColumn("dbo.Feedbacks", "EmployeeId", c => c.Int());
            DropColumn("dbo.Feedbacks", "CreatedOn");
            CreateIndex("dbo.Feedbacks", "EmployeeId");
            AddForeignKey("dbo.Feedbacks", "EmployeeId", "dbo.Employees", "Id");
        }
    }
}
