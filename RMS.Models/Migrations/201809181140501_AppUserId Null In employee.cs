namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserIdNullInemployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "AppUserId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "AppUserId" });
            AlterColumn("dbo.Employees", "AppUserId", c => c.Int());
            CreateIndex("dbo.Employees", "AppUserId");
            AddForeignKey("dbo.Employees", "AppUserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "AppUserId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "AppUserId" });
            AlterColumn("dbo.Employees", "AppUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "AppUserId");
            AddForeignKey("dbo.Employees", "AppUserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
