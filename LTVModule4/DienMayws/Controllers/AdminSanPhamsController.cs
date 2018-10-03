using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;
using System.IO;

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
        #region Xử lý upload photo
        // GET: AdminSanPham/Upload/5
        public ActionResult Upload(int? id)
        {
            if (id == null || id < 1) return RedirectToAction("Index");
            try
            {
                SanPham sanPham = db.SanPhams.Find(id);
                if (sanPham == null) throw new Exception("Sản phẩm ID=" + id + " không tồn tại.");
                ViewBag.SanPhamID = id;
                ViewBag.ThongTinSanPham = sanPham.Ten;
                return View();
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "Không truy cập được dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);
            }
        }

        // Post: AdminSanPham/Upload/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(int sanphamid, HttpPostedFileBase hinh1, HttpPostedFileBase hinh2, HttpPostedFileBase hinh3)
        {
            try
            {
                if ((hinh1 ?? hinh2 ?? hinh3) == null)
                {
                    ViewBag.SanPhamID = sanphamid;
                    ViewBag.ThongBao = "Bạn chưa chọn tập tin";
                    return View();
                }
                SanPham sanpham = db.SanPhams.Find(sanphamid);
                string photosURL = Server.MapPath("~/Photos/");
                if (hinh1 != null && hinh1.ContentLength > 0)
                {
                    string kieu = Path.GetExtension(hinh1.FileName);
                    string tenHinh = string.Format("{0}-1{1}", sanpham.SanPhamID, kieu);
                    hinh1.SaveAs(photosURL + tenHinh);
                    if (!string.IsNullOrEmpty(sanpham.Hinh1) && tenHinh != sanpham.Hinh1)
                    {
                        if (System.IO.File.Exists(photosURL + sanpham.Hinh1))
                            System.IO.File.Delete(photosURL + sanpham.Hinh1);


                    }
                    sanpham.Hinh1 = tenHinh;
                }
                if (hinh2 != null && hinh2.ContentLength > 0)
                {
                    string kieu = Path.GetExtension(hinh2.FileName);
                    string tenHinh = string.Format("{0}-2{1}", sanpham.SanPhamID, kieu);
                    hinh2.SaveAs(photosURL + tenHinh);
                    if (!string.IsNullOrEmpty(sanpham.Hinh2) && tenHinh != sanpham.Hinh2)
                    {
                        if (System.IO.File.Exists(photosURL + sanpham.Hinh2))
                            System.IO.File.Delete(photosURL + sanpham.Hinh2);


                    }
                    sanpham.Hinh2 = tenHinh;
                }
                if (hinh3 != null && hinh3.ContentLength > 0)
                {
                    string kieu = Path.GetExtension(hinh3.FileName);
                    string tenHinh = string.Format("{0}-3{1}", sanpham.SanPhamID, kieu);
                    hinh3.SaveAs(photosURL + tenHinh);
                    if (!string.IsNullOrEmpty(sanpham.Hinh3) && tenHinh != sanpham.Hinh3)
                    {
                        if (System.IO.File.Exists(photosURL + sanpham.Hinh3))
                            System.IO.File.Delete(photosURL + sanpham.Hinh3);


                    }
                    sanpham.Hinh3 = tenHinh;
                }
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "Lỗi server.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);
            }

        }

        #endregion
        #region sử dụng remote attribute
        public JsonResult KiemTraTrungTenSanPham(string Ten,int? SanPhamID)
            {
            int kq = 0;
            if(SanPhamID==null)
            {
                //Thêm
                kq = db.SanPhams.Count(p => p.Ten == Ten);
            }
            else
            {
                //Sửa
                kq = db.SanPhams.Count(p => p.SanPhamID != SanPhamID && p.Ten == Ten);
            }
            if(kq>0)
            {
                return Json("Tên Sản Phẩm Bị Trùng", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
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
