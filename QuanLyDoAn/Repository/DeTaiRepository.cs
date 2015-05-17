using QuanLyDoAn.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDoAn.Repository
{
    public class DeTaiRepository
    {
        private DoAnDbContext context;

        public DeTaiRepository()
        {
            context = new DoAnDbContext();
        }

        private IEnumerable<DeTaiViewModel> ToViewModel(IEnumerable<DeTai> data)
        {
            foreach (var item in data)
            {
                yield return new DeTaiViewModel
                {
                    MaDT = item.MaDT,
                    TenDT = item.TenDT,
                    NoiDung = item.NoiDung,
                    MoTa = item.MoTa,
                    MaxSV = item.MaxSV ?? 0,
                    MaBM = item.MaBM,
                    DaDK = item.DangKies.Count
                };
            }
        }
    }

    public class DeTaiViewModel
    {
        public string MaDT { get; set; }
        public string TenDT { get; set; }
        public string NoiDung { get; set; }
        public string MoTa { get; set; }
        public int MaxSV { get; set; }
        public string MaBM { get; set; }
        public int DaDK { get; set; }
    }

}