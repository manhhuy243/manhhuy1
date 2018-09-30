using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;

namespace DienMayws.Controllers
{
    public class AdminSanPhamsController : Controller
    {
        private DienMayDBContext db = new DienMayDBContext();

        // GET: AdminSanPhams/List
        public ActionResult List()
        {
            var query = db.SanPhams
                          .Include(s => s.NhaSanXuat);
            ViewBag.SanPhams = query;
            if(Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial");
            }
            return View();
        }
        #region
        // GET: AdminSanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: AdminSanPhams/Create
        public ActionResult Create()
        {
            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten");
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten");
            return View();
        }

        // POST: AdminSanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SanPhamID,NhaSanXuatID,LoaiID,Ten,GiaBan,MoTa,XuatXu,TrangThai,KichCo,BangTan,Camera,GPRS,DacTinh,BiDanh,Hinh1,Hinh2,Hinh3")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten", sanPham.LoaiID);
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
            return View(sanPham);
        }

        // GET: AdminSanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten", sanPham.LoaiID);
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
            return View(sanPham);
        }

        // POST: AdminSanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SanPhamID,NhaSanXuatID,LoaiID,Ten,GiaBan,MoTa,XuatXu,TrangThai,KichCo,BangTan,Camera,GPRS,DacTinh,BiDanh,Hinh1,Hinh2,Hinh3")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten", sanPham.LoaiID);
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
            return View(sanPham);
        }

        // GET: AdminSanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: AdminSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
#endregion
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
