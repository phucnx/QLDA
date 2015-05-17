namespace QuanLyDoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_something : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BoMon", "TrangThai");
            DropColumn("dbo.DeTai", "TrangThai");
            DropColumn("dbo.GiangVien", "TrangThai");
            DropColumn("dbo.SinhVien", "TrangThai");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SinhVien", "TrangThai", c => c.Int());
            AddColumn("dbo.GiangVien", "TrangThai", c => c.Int());
            AddColumn("dbo.DeTai", "TrangThai", c => c.Int());
            AddColumn("dbo.BoMon", "TrangThai", c => c.Int());
        }
    }
}
