namespace QuanLyDoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_DangKy_Table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DangKy", "DiemQT", c => c.Decimal(precision: 3, scale: 1));
            AlterColumn("dbo.DangKy", "DiemThi", c => c.Decimal(precision: 3, scale: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DangKy", "DiemThi", c => c.Decimal(nullable: false, precision: 3, scale: 1));
            AlterColumn("dbo.DangKy", "DiemQT", c => c.Decimal(nullable: false, precision: 3, scale: 1));
        }
    }
}
