namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeedbackModeladdedEmployeeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "EmployeeId", c => c.Int());
            CreateIndex("dbo.Feedbacks", "EmployeeId");
            AddForeignKey("dbo.Feedbacks", "EmployeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Feedbacks", new[] { "EmployeeId" });
            DropColumn("dbo.Feedbacks", "EmployeeId");
        }
    }
}
