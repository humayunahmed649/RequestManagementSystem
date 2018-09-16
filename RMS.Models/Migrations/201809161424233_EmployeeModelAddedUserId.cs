namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeModelAddedUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "AppUserId", c => c.Int(nullable: true));
            CreateIndex("dbo.Employees", "AppUserId");
            AddForeignKey("dbo.Employees", "AppUserId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "AppUserId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "AppUserId" });
            DropColumn("dbo.Employees", "AppUserId");
        }
    }
}
