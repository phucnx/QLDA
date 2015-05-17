namespace QuanLyDoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoMon",
                c => new
                    {
                        MaBM = c.String(nullable: false, maxLength: 5, unicode: false),
                        TenBM = c.String(nullable: false, maxLength: 200),
                        MoTa = c.String(storeType: "ntext"),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MaBM);
            
            CreateTable(
                "dbo.DeTai",
                c => new
                    {
                        MaDT = c.String(nullable: false, maxLength: 5, unicode: false),
                        TenDT = c.String(nullable: false, maxLength: 200),
                        NoiDung = c.String(nullable: false, storeType: "ntext"),
                        MoTa = c.String(maxLength: 200),
                        MaxSV = c.Int(),
                        TrangThai = c.Int(),
                        MaBM = c.String(nullable: false, maxLength: 5, unicode: false),
                    })
                .PrimaryKey(t => t.MaDT)
                .ForeignKey("dbo.BoMon", t => t.MaBM)
                .Index(t => t.MaBM);
            
            CreateTable(
                "dbo.DangKy",
                c => new
                    {
                        MaDT = c.String(nullable: false, maxLength: 5, unicode: false),
                        MSSV = c.String(nullable: false, maxLength: 8, fixedLength: true, unicode: false),
                        MSGV = c.String(nullable: false, maxLength: 8, unicode: false),
                        DiemQT = c.Decimal(nullable: false, precision: 3, scale: 1),
                        DiemThi = c.Decimal(nullable: false, precision: 3, scale: 1),
                    })
                .PrimaryKey(t => new { t.MaDT, t.MSSV, t.MSGV })
                .ForeignKey("dbo.GiangVien", t => t.MSGV)
                .ForeignKey("dbo.SinhVien", t => t.MSSV)
                .ForeignKey("dbo.DeTai", t => t.MaDT)
                .Index(t => t.MaDT)
                .Index(t => t.MSSV)
                .Index(t => t.MSGV);
            
            CreateTable(
                "dbo.GiangVien",
                c => new
                    {
                        MSGV = c.String(nullable: false, maxLength: 8, unicode: false),
                        HoTen = c.String(nullable: false, maxLength: 50),
                        NgaySinh = c.DateTime(nullable: false, storeType: "date"),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MSGV);
            
            CreateTable(
                "dbo.SinhVien",
                c => new
                    {
                        MSSV = c.String(nullable: false, maxLength: 8, fixedLength: true, unicode: false),
                        HoTen = c.String(nullable: false, maxLength: 50),
                        NgaySinh = c.DateTime(nullable: false, storeType: "date"),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MSSV);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeTai", "MaBM", "dbo.BoMon");
            DropForeignKey("dbo.DangKy", "MaDT", "dbo.DeTai");
            DropForeignKey("dbo.DangKy", "MSSV", "dbo.SinhVien");
            DropForeignKey("dbo.DangKy", "MSGV", "dbo.GiangVien");
            DropIndex("dbo.DangKy", new[] { "MSGV" });
            DropIndex("dbo.DangKy", new[] { "MSSV" });
            DropIndex("dbo.DangKy", new[] { "MaDT" });
            DropIndex("dbo.DeTai", new[] { "MaBM" });
            DropTable("dbo.SinhVien");
            DropTable("dbo.GiangVien");
            DropTable("dbo.DangKy");
            DropTable("dbo.DeTai");
            DropTable("dbo.BoMon");
        }
    }
}
