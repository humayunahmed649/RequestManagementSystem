namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_passengerQty_in_Requisition_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requisitions", "PassengerQty", c => c.Int(nullable: false));
            AlterColumn("dbo.Requisitions", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requisitions", "Description", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.Requisitions", "PassengerQty");
        }
    }
}
