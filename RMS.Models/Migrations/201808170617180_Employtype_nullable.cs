namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employtype_nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            AlterColumn("dbo.Employees", "EmployeeTypeId", c => c.Int());
            CreateIndex("dbo.Employees", "EmployeeTypeId");
            AddForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            AlterColumn("dbo.Employees", "EmployeeTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "EmployeeTypeId");
            AddForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes", "Id", cascadeDelete: true);
        }
    }
}
