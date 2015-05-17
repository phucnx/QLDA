namespace QuanLyDoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_SinhVien : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SinhVien", "NgaySinh", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SinhVien", "NgaySinh", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
