namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeType_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "EmployeeTypeId", c => c.Int(nullable: true));
            CreateIndex("dbo.Employees", "EmployeeTypeId");
            AddForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes", "Id", cascadeDelete: false);
            DropColumn("dbo.Employees", "EmployeeTypes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "EmployeeTypes", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            DropColumn("dbo.Employees", "EmployeeTypeId");
            DropTable("dbo.EmployeeTypes");
        }
    }
}
