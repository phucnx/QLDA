namespace QuanLyDoAn.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKy")]
    public partial class DangKy
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        [Display(Name = "Mã đề tài")]
        public string MaDT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        [Display(Name = "Mã số SV")]
        public string MSSV { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        [Display(Name = "Mã số GV")]
        public string MSGV { get; set; }

        [Display(Name = "Điểm QT")]
        [Range(0.0, 10.0,ErrorMessage="Điểm không hợp lệ")]
        public decimal? DiemQT { get; set; }

        [Display(Name = "Điểm thi")]
        [Range(0.0, 10.0,ErrorMessage="Điểm không hợp lệ")]
        public decimal? DiemThi { get; set; }

        public int? TrangThai { get; set; }

        public virtual DeTai DeTai { get; set; }

        public virtual GiangVien GiaoVien { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
