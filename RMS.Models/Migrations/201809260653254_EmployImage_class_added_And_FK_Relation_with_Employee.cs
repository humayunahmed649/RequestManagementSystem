namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployImage_class_added_And_FK_Relation_with_Employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 1000),
                        ImageByte = c.Binary(),
                        ImagePath = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "EmployeeImageId", c => c.Int());
            CreateIndex("dbo.Employees", "EmployeeImageId");
            AddForeignKey("dbo.Employees", "EmployeeImageId", "dbo.EmployeeImages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "EmployeeImageId", "dbo.EmployeeImages");
            DropIndex("dbo.Employees", new[] { "EmployeeImageId" });
            DropColumn("dbo.Employees", "EmployeeImageId");
            DropTable("dbo.EmployeeImages");
        }
    }
}
