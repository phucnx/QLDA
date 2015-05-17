using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QuanLyDoAn.Models;
using QuanLyDoAn.Models.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace QuanLyDoAn.Controllers.BackEnd
{
    public class QLSinhVienController : Controller
    {
        private DoAnDbContext db = new DoAnDbContext();
        // GET: QLSinhVien
        public ActionResult Index()
        {
            return View(db.SinhViens.ToList());
        }

        // GET: QLSinhVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: QLSinhVien/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLSinhVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "MSSV,HoTen,NgaySinh,Password,ConfirmPassword")] RegisterStudent student)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = new ApplicationUser { UserName = student.MSSV, Email = student.MSSV + "@student.hust.edu.vn" };
                    var result = UserManager.Create(user, student.Password);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "Student");
                        var sv = new SinhVien { MSSV = student.MSSV, NgaySinh = student.NgaySinh, HoTen = student.HoTen };
                        try
                        {
                            db.SinhViens.Add(sv);
                            db.SaveChanges();
                            scope.Complete();
                            TempData["Message"] = "Thành công";
                            return RedirectToAction("Create");
                        }
                        catch (Exception exp)
                        {
                            ModelState.AddModelError(string.Empty, exp.Message);
                            return View(student);
                        }
                    }
                    else
                    {
                        AddErrors(result);
                        return View(student);
                    }
                }
            }
            return View(student);
        }

        // GET: QLSinhVien/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: QLSinhVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "MSSV,HoTen,NgaySinh")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(sinhVien);
        }

        // GET: QLSinhVien/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: QLSinhVien/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            using (var scope = new TransactionScope())
            {
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var sinhVien = db.SinhViens.Find(id);
                var user = UserManager.FindByName(sinhVien.MSSV);
                var result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    var dangkys = db.DangKies.Where(d => d.MSSV == id).ToList();
                    db.DangKies.RemoveRange(dangkys);
                    db.SinhViens.Remove(sinhVien);
                    db.SaveChanges();
                    scope.Complete();
                    TempData["Message"] = "Xóa thành công";
                }
                else
                {
                    TempData["Message"] = "Đã có lỗi xảy ra";
                }
            }
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
