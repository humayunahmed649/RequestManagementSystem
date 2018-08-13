namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Division_District_Upazila_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DivisionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        BnName = c.String(nullable: false, maxLength: 50),
                        Lat = c.Single(nullable: false),
                        Lon = c.Single(nullable: false),
                        Website = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        BnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Upazilas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        BnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Upazilas", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Districts", "DivisionId", "dbo.Divisions");
            DropIndex("dbo.Upazilas", new[] { "DistrictId" });
            DropIndex("dbo.Districts", new[] { "DivisionId" });
            DropTable("dbo.Upazilas");
            DropTable("dbo.Divisions");
            DropTable("dbo.Districts");
        }
    }
}
