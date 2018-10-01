namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification_table_changed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "SenderText", c => c.String());
            AddColumn("dbo.Notifications", "SenderNotifyDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "ControllerText", c => c.String());
            AddColumn("dbo.Notifications", "ControllerNotifyDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notifications", "Text");
            DropColumn("dbo.Notifications", "NotifyDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "NotifyDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "Text", c => c.String());
            DropColumn("dbo.Notifications", "ControllerNotifyDateTime");
            DropColumn("dbo.Notifications", "ControllerText");
            DropColumn("dbo.Notifications", "SenderNotifyDateTime");
            DropColumn("dbo.Notifications", "SenderText");
        }
    }
}
