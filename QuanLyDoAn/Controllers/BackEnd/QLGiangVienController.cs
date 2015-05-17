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
    public class QLGiangVienController : Controller
    {
        private DoAnDbContext db = new DoAnDbContext();

        // GET: QLGiangVien
        public ActionResult Index()
        {
            return View(db.GiangViens.ToList());
        }

        // GET: QLGiangVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = db.GiangViens.Find(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // GET: QLGiangVien/Create
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QLGiangVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "MSGV,HoTen,NgaySinh,Password,ConfirmPassword")] RegisterTeacher teacher)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var user = new ApplicationUser { UserName = teacher.MSGV, Email = teacher.MSGV + "@teacher.hust.edu.vn" };
                    var result = UserManager.Create(user, teacher.Password);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(UserManager.FindByName(user.UserName).Id, "Teacher");
                        var sv = new GiangVien { MSGV = teacher.MSGV, NgaySinh = teacher.NgaySinh, HoTen = teacher.HoTen };
                        try
                        {
                            db.GiangViens.Add(sv);
                            db.SaveChanges();
                            scope.Complete();
                            ViewBag.Message = "Thành công";
                            return View("Create");
                        }
                        catch (Exception exp)
                        {
                            ModelState.AddModelError(string.Empty, exp.Message);
                            return View(teacher);
                        }
                    }
                    else
                    {
                         AddErrors(result);
                        return View(teacher);
                    }
                }
            }
            return View(teacher);
        }

        // GET: QLGiangVien/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = db.GiangViens.Find(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // POST: QLGiangVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "MSGV,HoTen,NgaySinh")] GiangVien giangVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangVien).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(giangVien);
        }

        // GET: QLGiangVien/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiangVien giangVien = db.GiangViens.Find(id);
            if (giangVien == null)
            {
                return HttpNotFound();
            }
            return View(giangVien);
        }

        // POST: QLGiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            using (var scope = new TransactionScope())
            {
                var UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var giangVien = db.GiangViens.Find(id);
                var user = UserManager.FindByName(giangVien.MSGV);
                var result = UserManager.Delete(user);
                if (result.Succeeded)
                {
                    var dangkys = db.DangKies.Where(d => d.MSGV == id).ToList();
                    db.DangKies.RemoveRange(dangkys);
                    db.GiangViens.Remove(giangVien);
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
