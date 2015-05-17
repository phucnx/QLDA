namespace QuanLyDoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_dangky : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DangKy", "TrangThai", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DangKy", "TrangThai");
        }
    }
}
