namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignRequisitionAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignRequisitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequisitionId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.RequisitionId)
                .Index(t => t.VehicleId)
                .Index(t => t.EmployeeId);

            DropColumn("dbo.Employees", "HouseNo");
            DropColumn("dbo.Employees", "RoadNo");
            DropColumn("dbo.Employees", "FloorNo");
            DropColumn("dbo.Employees", "PostOffice");
            DropColumn("dbo.Employees", "District");
            DropColumn("dbo.Employees", "Division");

            AddColumn("dbo.Employees", "PermanentAddress", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Employees", "PresentAddress", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Employees", "EmployeeTypes", c => c.String(nullable: false, maxLength: 50));

            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignRequisitions", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.AssignRequisitions", "RequisitionId", "dbo.Requisitions");
            DropForeignKey("dbo.AssignRequisitions", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.AssignRequisitions", new[] { "EmployeeId" });
            DropIndex("dbo.AssignRequisitions", new[] { "VehicleId" });
            DropIndex("dbo.AssignRequisitions", new[] { "RequisitionId" });

            AlterColumn("dbo.Employees", "EmployeeTypes", c => c.String());
            DropColumn("dbo.Employees", "PresentAddress");
            DropColumn("dbo.Employees", "PermanentAddress");
            AddColumn("dbo.Employees", "HouseNo", c => c.String());
            AddColumn("dbo.Employees", "RoadNo", c => c.String());
            AddColumn("dbo.Employees", "FloorNo", c => c.String());
            AddColumn("dbo.Employees", "PostOffice", c => c.String());
            AddColumn("dbo.Employees", "District", c => c.String());
            AddColumn("dbo.Employees", "Division", c => c.String());

            DropTable("dbo.AssignRequisitions");
        }
    }
}
