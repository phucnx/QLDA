namespace QuanLyDoAn.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DoAnDbContext : DbContext
    {
        public DoAnDbContext()
            : base("name=DoAnDBContext")
        {
        }

        public virtual DbSet<BoMon> BoMons { get; set; }
        public virtual DbSet<DangKy> DangKies { get; set; }
        public virtual DbSet<DeTai> DeTais { get; set; }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoMon>()
                .Property(e => e.MaBM)
                .IsUnicode(false);

            modelBuilder.Entity<BoMon>()
                .HasMany(e => e.DeTais)
                .WithRequired(e => e.BoMon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.MaDT)
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.MSSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.MSGV)
                .IsUnicode(false);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.DiemQT)
                .HasPrecision(3, 1);

            modelBuilder.Entity<DangKy>()
                .Property(e => e.DiemThi)
                .HasPrecision(3, 1);

            modelBuilder.Entity<DeTai>()
                .Property(e => e.MaDT)
                .IsUnicode(false);

            modelBuilder.Entity<DeTai>()
                .Property(e => e.MaBM)
                .IsUnicode(false);

            modelBuilder.Entity<DeTai>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.DeTai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GiangVien>()
                .Property(e => e.MSGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiangVien>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.GiaoVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MSSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.DangKies)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);
        }
    }
}
