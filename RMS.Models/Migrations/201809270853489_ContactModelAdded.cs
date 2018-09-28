namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactModels");
        }
    }
}
