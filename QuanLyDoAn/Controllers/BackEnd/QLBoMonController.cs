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
    public class QLBoMonController : Controller
    {
        private DoAnDbContext db = new DoAnDbContext();

        // GET: QLBoMon
        public ActionResult Index()
        {
            return View(db.BoMons.ToList());
        }

        // GET: QLBoMon/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoMon boMon = db.BoMons.Find(id);
            if (boMon == null)
            {
                return HttpNotFound();
            }
            return View(boMon);
        }

        // GET: QLBoMon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLBoMon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBM,TenBM,MoTa")] BoMon boMon)
        {
            if (ModelState.IsValid)
            {
                db.BoMons.Add(boMon);
                db.SaveChanges();
                TempData["Message"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            return View(boMon);
        }

        // GET: QLBoMon/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoMon boMon = db.BoMons.Find(id);
            if (boMon == null)
            {
                return HttpNotFound();
            }
            return View(boMon);
        }

        // POST: QLBoMon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBM,TenBM,MoTa")] BoMon boMon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boMon).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(boMon);
        }

        // GET: QLBoMon/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoMon boMon = db.BoMons.Find(id);
            if (boMon == null)
            {
                return HttpNotFound();
            }
            return View(boMon);
        }

        // POST: QLBoMon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BoMon boMon = db.BoMons.Find(id);
            var detais = db.DeTais.Where(x => x.MaBM == id).ToList();
            var dangkys = db.DangKies.Where(x => x.DeTai.MaBM == id).ToList();
            db.DangKies.RemoveRange(dangkys);
            db.DeTais.RemoveRange(detais);
            db.BoMons.Remove(boMon);
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
