namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignModelChangeDriveridDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AssignRequisitions", "DriverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssignRequisitions", "DriverId", c => c.Int(nullable: false));
        }
    }
}
