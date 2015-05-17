namespace QuanLyDoAn.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeTai")]
    public partial class DeTai
    {
        public DeTai()
        {
            DangKies = new HashSet<DangKy>();
        }

        [Key]
        [StringLength(5)]
        [Display(Name = "Mã đề tài")]
        public string MaDT { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tên đề tài")]
        public string TenDT { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        [StringLength(200)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Display(Name = "Max SV")]
        public int? MaxSV { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Mã bộ môn")]
        public string MaBM { get; set; }

        public virtual BoMon BoMon { get; set; }

        public virtual ICollection<DangKy> DangKies { get; set; }
    }
}
