namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleType_and_Vehicle_validation_added : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "BrandName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Vehicles", "ModelName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Vehicles", "RegNo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Vehicles", "ChassisNo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.VehicleTypes", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleTypes", "Name", c => c.String());
            AlterColumn("dbo.Vehicles", "ChassisNo", c => c.String());
            AlterColumn("dbo.Vehicles", "RegNo", c => c.String());
            AlterColumn("dbo.Vehicles", "ModelName", c => c.String());
            AlterColumn("dbo.Vehicles", "BrandName", c => c.String());
        }
    }
}
