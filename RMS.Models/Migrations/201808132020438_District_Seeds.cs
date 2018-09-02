namespace RMS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class District_Seeds : DbMigration
    {
        public override void Up()
        {
            Sql(@"DELETE FROM Districts;
                   DBCC CHECKIDENT ('Districts', RESEED, 0)");

            Sql(@"SET IDENTITY_INSERT [dbo].[Districts] ON 

            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (1, 3, N'Dhaka', N'ঢাকা', 23.7115253, 90.4111451, N'www.dhaka.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (2, 3, N'Faridpur', N'ফরিদপুর', 23.6070822, 89.8429406, N'www.faridpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (3, 3, N'Gazipur', N'গাজীপুর', 24.0022858, 90.4264283, N'www.gazipur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (4, 3, N'Gopalganj', N'গোপালগঞ্জ', 23.0050857, 89.8266059, N'www.gopalganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (5, 8, N'Jamalpur', N'জামালপুর', 24.937533, 89.937775, N'www.jamalpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (6, 3, N'Kishoreganj', N'কিশোরগঞ্জ', 24.444937, 90.776575, N'www.kishoreganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (7, 3, N'Madaripur', N'মাদারীপুর', 23.164102, 90.1896805, N'www.madaripur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (8, 3, N'Manikganj', N'মানিকগঞ্জ', 0, 0, N'www.manikganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (9, 3, N'Munshiganj', N'মুন্সিগঞ্জ', 0, 0, N'www.munshiganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (10, 8, N'Mymensingh', N'ময়মনসিংহ', 0, 0, N'www.mymensingh.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (11, 3, N'Narayanganj', N'নারায়াণগঞ্জ', 23.63366, 90.496482, N'www.narayanganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (12, 3, N'Narsingdi', N'নরসিংদী', 23.932233, 90.71541, N'www.narsingdi.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (13, 8, N'Netrokona', N'নেত্রকোণা', 24.870955, 90.727887, N'www.netrokona.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (14, 3, N'Rajbari', N'রাজবাড়ি', 23.7574305, 89.6444665, N'www.rajbari.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (15, 3, N'Shariatpur', N'শরীয়তপুর', 0, 0, N'www.shariatpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (16, 8, N'Sherpur', N'শেরপুর', 25.0204933, 90.0152966, N'www.sherpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (17, 3, N'Tangail', N'টাঙ্গাইল', 0, 0, N'www.tangail.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (18, 5, N'Bagura', N'বগুড়া', 24.8465228, 89.377755, N'www.bogra.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (19, 5, N'Joypurhat', N'জয়পুরহাট', 0, 0, N'www.joypurhat.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (20, 5, N'Naogaon', N'নওগাঁ', 0, 0, N'www.naogaon.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (21, 5, N'Natore', N'নাটোর', 24.420556, 89.000282, N'www.natore.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (22, 5, N'Nawabganj', N'নবাবগঞ্জ', 24.5965034, 88.2775122, N'www.chapainawabganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (23, 5, N'Pabna', N'পাবনা', 23.998524, 89.233645, N'www.pabna.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (24, 5, N'Rajshahi', N'রাজশাহী', 0, 0, N'www.rajshahi.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (25, 5, N'Sirajgonj', N'সিরাজগঞ্জ', 24.4533978, 89.7006815, N'www.sirajganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (26, 6, N'Dinajpur', N'দিনাজপুর', 25.6217061, 88.6354504, N'www.dinajpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (27, 6, N'Gaibandha', N'গাইবান্ধা', 25.328751, 89.528088, N'www.gaibandha.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (28, 6, N'Kurigram', N'কুড়িগ্রাম', 25.805445, 89.636174, N'www.kurigram.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (29, 6, N'Lalmonirhat', N'লালমনিরহাট', 0, 0, N'www.lalmonirhat.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (30, 6, N'Nilphamari', N'নীলফামারী', 25.931794, 88.856006, N'www.nilphamari.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (31, 6, N'Panchagarh', N'পঞ্চগড়', 26.3411, 88.5541606, N'www.panchagarh.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (32, 6, N'Rangpur', N'রংপুর', 25.7558096, 89.244462, N'www.rangpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (33, 6, N'Thakurgaon', N'ঠাকুরগাঁও', 26.0336945, 88.4616834, N'www.thakurgaon.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (34, 1, N'Barguna', N'বরগুনা', 0, 0, N'www.barguna.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (35, 1, N'Barishal', N'বরিশাল', 0, 0, N'www.barisal.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (36, 1, N'Bhola', N'ভোলা', 22.685923, 90.648179, N'www.bhola.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (37, 1, N'Jhalokati', N'ঝালকাঠি', 0, 0, N'www.jhalakathi.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (38, 1, N'Patuakhali', N'পটুয়াখালী', 22.3596316, 90.3298712, N'www.patuakhali.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (39, 1, N'Pirojpur', N'পিরোজপুর', 0, 0, N'www.pirojpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (40, 2, N'Bandarban', N'বান্দরবান', 22.1953275, 92.2183773, N'www.bandarban.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (41, 2, N'Brahmanbaria', N'ব্রাহ্মণবাড়িয়া', 23.9570904, 91.1119286, N'www.brahmanbaria.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (42, 2, N'Chandpur', N'চাঁদপুর', 23.2332585, 90.6712912, N'www.chandpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (43, 2, N'Chattagram', N'চট্টগ্রাম', 22.335109, 91.834073, N'www.chittagong.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (44, 2, N'Kumilla', N'কুমিল্লা', 23.4682747, 91.1788135, N'www.comilla.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (45, 2, N'Cox''s Bazar', N'কক্স বাজার', 0, 0, N'www.coxsbazar.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (46, 2, N'Feni', N'ফেনী', 23.023231, 91.3840844, N'www.feni.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (47, 2, N'Khagrachari', N'খাগড়াছড়ি', 23.119285, 91.984663, N'www.khagrachhari.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (48, 2, N'Lakshmipur', N'লক্ষ্মীপুর', 22.942477, 90.841184, N'www.lakshmipur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (49, 2, N'Noakhali', N'নোয়াখালী', 22.869563, 91.099398, N'www.noakhali.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (50, 2, N'Rangamati', N'রাঙ্গামাটি', 0, 0, N'www.rangamati.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (51, 7, N'Habiganj', N'হবিগঞ্জ', 24.374945, 91.41553, N'www.habiganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (52, 7, N'Maulvibazar', N'মৌলভীবাজার', 24.482934, 91.777417, N'www.moulvibazar.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (53, 7, N'Sunamganj', N'সুনামগঞ্জ', 25.0658042, 91.3950115, N'www.sunamganj.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (54, 7, N'Sylhet', N'সিলেট', 24.8897956, 91.8697894, N'www.sylhet.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (55, 4, N'Bagerhat', N'বাগেরহাট', 22.651568, 89.785938, N'www.bagerhat.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (56, 4, N'Chuadanga', N'চুয়াডাঙ্গা', 23.6401961, 88.841841, N'www.chuadanga.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (57, 4, N'Jashore', N'যশোর', 23.16643, 89.2081126, N'www.jessore.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (58, 4, N'Jhenaidah', N'ঝিনাইদহ', 23.5448176, 89.1539213, N'www.jhenaidah.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (59, 4, N'Khulna', N'খুলনা', 22.815774, 89.568679, N'www.khulna.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (60, 4, N'Kushtia', N'কুষ্টিয়া', 23.901258, 89.120482, N'www.kushtia.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (61, 4, N'Magura', N'মাগুরা', 23.487337, 89.419956, N'www.magura.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (62, 4, N'Meherpur', N'মেহেরপুর', 23.762213, 88.631821, N'www.meherpur.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (63, 4, N'Narail', N'নড়াইল', 23.172534, 89.512672, N'www.narail.gov.bd')
            INSERT [dbo].[Districts] ([Id], [DivisionId], [Name], [BnName], [Lat], [Lon], [Website]) VALUES (64, 4, N'Satkhira', N'সাতক্ষীরা', 0, 0, N'www.satkhira.gov.bd')
            SET IDENTITY_INSERT[dbo].[Districts] OFF");
        }
        
        public override void Down()
        {
            Sql(@"DELETE FROM Districts;
                   DBCC CHECKIDENT ('Districts', RESEED, 0)");
        }
    }
}
