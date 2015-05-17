namespace QuanLyDoAn.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoMon")]
    public partial class BoMon
    {
        public BoMon()
        {
            DeTais = new HashSet<DeTai>();
        }

        [Key]
        [StringLength(5)]
        [Display(Name = "Mã bộ môn")]
        public string MaBM { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name="Tên bộ môn")]
        public string TenBM { get; set; }

        [Display(Name = "Mô tả")]
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public virtual ICollection<DeTai> DeTais { get; set; }
    }
}
