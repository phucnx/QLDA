using QuanLyDoAn.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDoAn.Controllers.FrontEnd
{
    public class DangKyController : Controller
    {
        private DoAnDbContext context = null;

        public DangKyController()
        {
            context = new DoAnDbContext();
        }
        public ActionResult Index()
        {
            var mssv = User.Identity.Name;
            var data = context.DangKies.Where(x => x.MSSV == mssv).FirstOrDefault();
            if (data != null)
            {
                ViewBag.Data = ToViewModel(data);
                if (data.TrangThai == 1)
                {
                    ViewBag.Success = data.DeTai.TenDT;
                }
            }
            ViewBag.IsStudent = User.IsInRole("Student");
            return View(context.DeTais.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public ActionResult Register([Bind(Include = "MaDT")]DangKy model)
        {
            if (ModelState.IsValid)
            {
                var dangKy = context.DangKies.Where(d => d.MaDT == model.MaDT);
                if (dangKy.Count() != 0)
                {
                    model.MSGV = dangKy.First().MSGV;
                }
                else
                {
                    model.MSGV = context.GiangViens.OrderBy(g => g.MSGV).Skip(new Random().Next(0, context.GiangViens.Count())).First().MSGV;
                }
                model.MSSV = User.Identity.Name;
                var detai = context.DeTais.Find(model.MaDT);
                var maxSV = detai.MaxSV;
                var count = detai.DangKies.Count;
                model.TrangThai = (maxSV == null || maxSV > count) ? 1 : 2;
                var result = ToViewModel(context.DangKies.Add(model));
                context.SaveChanges();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public void Remove()
        {
            var mssv = User.Identity.Name;
            var data = context.DangKies.Where(m => m.MSSV == mssv).First();
            context.Entry(data).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult ListHuongDan()
        {
            var teacher = context.GiangViens.Find(User.Identity.Name);
            var data = teacher.DangKies.Where(x => x.TrangThai == 1).GroupBy(t => t.DeTai);
            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult ChoDiem(string id)
        {
            var dk = context.DangKies.Where(x => x.MSSV == id && x.TrangThai == 1);
            if (dk.Count() == 0)
                return HttpNotFound();
            return View(dk.First());
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult ChoDiem([Bind(Include = "MSSV,MSGV,MaDT,DiemQT,DiemThi,TrangThai")] DangKy dk)
        {
            if (ModelState.IsValid)
            {
                context.Entry(dk).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                TempData["MSSV"] = dk.MSSV;
                return RedirectToAction("ListHuongDan");
            }
            return View(context.DangKies.Where(d => d.MSSV == dk.MSSV).First());
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public ActionResult XemDiem()
        {
            var mssv = User.Identity.Name;
            var data = context.DangKies.Where(x => x.MSSV == mssv && x.TrangThai == 1).FirstOrDefault();
            return View(data);
        }

        private DkViewModel ToViewModel(DangKy model)
        {
            return new DkViewModel
            {
                MaDT = model.MaDT,
                TenDT = model.DeTai.TenDT,
                BoMon = model.DeTai.BoMon.TenBM,
                NoiDung = model.DeTai.NoiDung,
                GVHD = context.GiangViens.Find(model.MSGV).HoTen,
                TrangThai = model.TrangThai == 1 ? "Thành công" : "Hết chỗ",
            };
        }
    }
    public class DkViewModel
    {
        public string MaDT { get; set; }
        public string TenDT { get; set; }
        public string BoMon { get; set; }
        public string NoiDung { get; set; }
        public string GVHD { get; set; }
        public string TrangThai { get; set; }
    }
}