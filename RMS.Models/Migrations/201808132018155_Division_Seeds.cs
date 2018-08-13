namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Division_Seeds : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Divisions] ON 

                INSERT [dbo].[Divisions] ([Id], [Name],[BnName]) VALUES 
                (1, N'Barishal', N'বরিশাল'),
                (2, N'Chattogram', N'চট্টগ্রাম'),
                (3, N'Dhaka', N'ঢাকা'),
                (4, N'Khulna', N'খুলনা'),
                (5, N'Rajshahi', N'রাজশাহী'),
                (6, N'Rangpur', N'রংপুর'),
                (7, N'Sylhet', N'সিলেট'),
                (8, N'Mymensingh', N'ময়মনসিংহ');

                SET IDENTITY_INSERT [dbo].[Divisions] OFF");
        }
        
        public override void Down()
        {
            Sql(@"DELETE FROM Divisions;
                   DBCC CHECKIDENT ('Divisions', RESEED, 0)");
        }
    }
}
