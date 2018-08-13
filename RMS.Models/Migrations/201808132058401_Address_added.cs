namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressType = c.String(nullable: false, maxLength: 250),
                        AddressLine1 = c.String(maxLength: 250),
                        AddressLine2 = c.String(nullable: false, maxLength: 250),
                        PostOffice = c.String(nullable: false, maxLength: 250),
                        PostCode = c.String(maxLength: 6),
                        DivisionId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        UpazilaId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Upazilas", t => t.UpazilaId, cascadeDelete: false)
                .Index(t => t.DivisionId)
                .Index(t => t.DistrictId)
                .Index(t => t.UpazilaId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "UpazilaId", "dbo.Upazilas");
            DropForeignKey("dbo.Addresses", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Addresses", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.Addresses", "DistrictId", "dbo.Districts");
            DropIndex("dbo.Addresses", new[] { "EmployeeId" });
            DropIndex("dbo.Addresses", new[] { "UpazilaId" });
            DropIndex("dbo.Addresses", new[] { "DistrictId" });
            DropIndex("dbo.Addresses", new[] { "DivisionId" });
            DropTable("dbo.Addresses");
        }
    }
}
