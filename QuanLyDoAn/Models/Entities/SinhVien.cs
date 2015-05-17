namespace QuanLyDoAn.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        public SinhVien()
        {
            DangKies = new HashSet<DangKy>();
        }

        [Key]
        [StringLength(8)]
        [Display(Name = "Mã số SV")]
        public string MSSV { get; set; }

        [Required(ErrorMessage="Vui lòng điền họ tên!")]
        [StringLength(50)]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        public virtual ICollection<DangKy> DangKies { get; set; }
    }
}
