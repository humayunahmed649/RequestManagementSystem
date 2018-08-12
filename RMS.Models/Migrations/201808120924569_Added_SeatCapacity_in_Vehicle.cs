namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_SeatCapacity_in_Vehicle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "SeatCapacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "SeatCapacity");
        }
    }
}
