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
            if (id == null||id<1)
            {
                return RedirectToAction("List");
            }
            try
            {
                SanPham sanPham = db.SanPhams.Find(id);
                if (sanPham == null) throw new Exception("Sản Phẩm ID= " + id + " không tồn tại");
                //khởi tạo nguồn dữ liệu cho các dropdownlist
                List<Loai> loaiItems = db.Loais.ToList();
                ViewBag.LoaiID = new SelectList(loaiItems, "LoaiID", "Ten", sanPham.LoaiID);
                List<NhaSanXuat> nsxItems = db.NhaSanXuats.ToList();
                ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
                var arrTrangThai = new[]
                {
                    new {TrangThaiID="",Ten=""},
                    new {TrangThaiID="bt",Ten="Bình Thường"},
                    new {TrangThaiID="nb",Ten="Nổi Bật"},
                    new {TrangThaiID="new",Ten="Mới Nhất"},
                    new {TrangThaiID="het",Ten="Hết Hàng"},
                };
                ViewBag.TrangThai = new SelectList(arrTrangThai, "TrangThaiID", "Ten", sanPham.TrangThai);
                //giả sử có thông tin cần bảo mật, ngăn không cho truyền lên view
                sanPham.XuatXu = null;
                sanPham.BiDanh = null;
                //chỉ định view mặc định hiển thị và truyền dữ liệu lên view
                return View(sanPham);
            }
            catch(Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
        }

        // POST: AdminSanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///giả sử chỉ cho hiệu chỉnh các thông tin sau: NhaSanXuat,LoaiID,Ten,GiaBan,TrangThai
        ///Chỉ cho post từ form về các thông tin: SanPhamID,NhaSanXuatID,LoaiID,Ten,GiaBan,TrangThai
        /// </summary>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SanPhamID,NhaSanXuatID,LoaiID,Ten,GiaBan,TrangThai")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sanphamHC = db.SanPhams.Find(sanPham.SanPhamID);
                    TryUpdateModel(sanphamHC, new string[] { "SanPhamID", "NhaSanXuatID", "LoaiID", "Ten", "GiaBan", "TrangThai" });
                    sanphamHC.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(sanphamHC.Ten);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //trường hợp dữ liệu nhập không hợp lệ(quay trở lại view cho clien xem thông thông báo lỗi)
                //khơi tạo nguồn dữ liệu cho các dropdownlist

                List<Loai> loaiItems = db.Loais.ToList();
                ViewBag.LoaiID = new SelectList(loaiItems, "LoaiID", "Ten", sanPham.LoaiID);
                List<NhaSanXuat> nsxItems = db.NhaSanXuats.ToList();
                ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
                var arrTrangThai = new[]
                {
                    new {TrangThaiID="",Ten=""},
                    new {TrangThaiID="bt",Ten="Bình Thường"},
                    new {TrangThaiID="nb",Ten="Nổi Bật"},
                    new {TrangThaiID="new",Ten="Mới Nhất"},
                    new {TrangThaiID="het",Ten="Hết Hàng"},
                };
                ViewBag.TrangThai = new SelectList(arrTrangThai, "TrangThaiID", "Ten", sanPham.TrangThai);
                return View(sanPham);
            }
            catch(Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
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
