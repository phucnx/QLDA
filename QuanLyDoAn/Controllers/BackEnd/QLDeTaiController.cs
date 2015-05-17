using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyDoAn.Models.Entities;

namespace QuanLyDoAn.Controllers.BackEnd
{
    public class QLDeTaiController : Controller
    {
        private DoAnDbContext db = new DoAnDbContext();

        // GET: QLDeTai
        public ActionResult Index()
        {
            var deTais = db.DeTais.Include(d => d.BoMon);
            return View(deTais.ToList());
        }

        // GET: QLDeTai/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeTai deTai = db.DeTais.Find(id);
            if (deTai == null)
            {
                return HttpNotFound();
            }
            return View(deTai);
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public ActionResult GetById(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deTai = db.DeTais.Find(id);
            var mssv = User.Identity.Name;
            if (db.DangKies.Where(m => m.MSSV == mssv).Count() > 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You must delete the current Project before register new one!");
            }
            if (deTai == null)
            {
                return HttpNotFound();
            }
            var data = new
            {
                MaDT = deTai.MaDT,
                TenDT = deTai.TenDT,
                MaxSV = deTai.MaxSV,
                DaDK = deTai.DangKies.Count,
                TenBM = deTai.BoMon.TenBM,
                MoTa = deTai.MoTa,
                NoiDung = deTai.NoiDung,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: QLDeTai/Create
        public ActionResult Create()
        {
            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM");
            return View();
        }

        // POST: QLDeTai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDT,TenDT,NoiDung,MoTa,MaxSV,MaBM")] DeTai deTai)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.DeTais.Add(deTai);
                    db.SaveChanges();
                    TempData["Message"] = "Thêm mới thành công";
                    return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("", exp.Message);
                    ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", deTai.MaBM);
                    return View(deTai);
                }

            }

            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", deTai.MaBM);
            return View(deTai);
        }

        // GET: QLDeTai/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeTai deTai = db.DeTais.Find(id);
            if (deTai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", deTai.MaBM);
            return View(deTai);
        }

        // POST: QLDeTai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDT,TenDT,NoiDung,MoTa,MaxSV,MaBM")] DeTai deTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deTai).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            ViewBag.MaBM = new SelectList(db.BoMons, "MaBM", "TenBM", deTai.MaBM);
            return View(deTai);
        }

        // GET: QLDeTai/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeTai deTai = db.DeTais.Find(id);
            if (deTai == null)
            {
                return HttpNotFound();
            }
            return View(deTai);
        }

        // POST: QLDeTai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DeTai deTai = db.DeTais.Find(id);
            var dangkys = db.DangKies.Where(d => d.MaDT == id).ToList();
            db.DangKies.RemoveRange(dangkys);
            db.DeTais.Remove(deTai);
            db.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
