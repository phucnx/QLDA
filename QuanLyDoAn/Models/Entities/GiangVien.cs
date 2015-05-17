namespace QuanLyDoAn.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiangVien")]
    public partial class GiangVien
    {
        public GiangVien()
        {
            DangKies = new HashSet<DangKy>();
        }

        [Key]
        [StringLength(8)]
        [Display(Name="Mã số GV")]
        public string MSGV { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        public virtual ICollection<DangKy> DangKies { get; set; }
    }
}
