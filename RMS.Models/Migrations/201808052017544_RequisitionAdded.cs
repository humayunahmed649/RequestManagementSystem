namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requisitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromPlace = c.String(),
                        DestinationPlace = c.String(),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        RequestFor = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisitions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Requisitions", new[] { "EmployeeId" });
            DropTable("dbo.Requisitions");
        }
    }
}
