namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplyModelAddedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Replies", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Replies", "CreatedOn");
        }
    }
}
