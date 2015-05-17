namespace QuanLyDoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_GiaoVien : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GiangVien", "NgaySinh", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GiangVien", "NgaySinh", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
