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
    [RoutePrefix("quan-ly/loai")]
    [Authorize]
    public class AdminLoaiController : Controller
    {
        
        private DienMayDBContext db = new DienMayDBContext();

        // GET: AdminLoai
        #region xem thông tin
        [Route("xem-danh-sach")]
        //GET:quan-ly/loai/xem-chi-tiet
        [AllowAnonymous]//không cần đăng nhập.
        public ActionResult Index()
        {
            //bẩy lỗi trên view
            IQueryable<Loai> query = db.Loais
                                       .Include(l => l.ChungLoai)
                                       .AsQueryable();
            ViewBag.LoaiAct = "active";
            return View(query);
        }
        [Route("xem-chi-tiet-{id}")]
        //GET:quan-ly/loai/xem-chi-tiet-5
        [Authorize(Roles ="QuanLy,NhanVien")]
        public ActionResult Details(int? id)
        {
            // nếu id = null trở về trang chủ
            if (id == null || id < 1) return RedirectToAction("Index");
            try
            {
                Loai loai = db.Loais
                              .Include(p => p.ChungLoai)
                              .SingleOrDefault(p => p.LoaiID == id);

                if (loai == null)
                {
                    throw new Exception("Loại ID: " + id + " Không tồn tại!");
                }
                return View(loai);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }

        }
        #endregion

        #region tạo 
        [Route("them-moi")]
        //GET:quan-ly/loai/them-moi
        [Authorize(Roles ="NhanVien")]
        public ActionResult Create()
        {
            try
            {
                //khởi tạo nguồn dữ liệu cho dropdownlist
                List<ChungLoai> chungLoaiItem = db.ChungLoais.ToList();
                ViewBag.ChungLoaiID = new SelectList(chungLoaiItem, "ChungLoaiID", "Ten");
                return View();
            }
            catch(Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
        }

        [Route("them-moi")]
        // POST: quan-ly/loai/them-moi
        [Authorize(Roles = "NhanVien")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]// chống mạo danh
        public ActionResult Create(Loai loai)
        {
            try
            {
                Loai l = db.Loais.SingleOrDefault(p => p.Ten == loai.Ten);
                if (l != null)
                {
                    ModelState.AddModelError("Ten", "Tên thiết bị này đã tồn tại.");
                }
                if (ModelState.IsValid)
                {
                    //trường hợp dữ liệu nhập hợp lệ(không vi phạm các kiểm tra cài đặt trong data model)
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Loais.Add(loai);
                    db.SaveChanges();
                    // lưu thành công
                    //điều hướng về action Index của controller điều hành
                    return RedirectToAction("Index");
                }
                //trường hợp dữ liệu nhập không hợp lệ
                //quay trở lại view và chuyền lại  dữ liệu
                List<ChungLoai> chungLoaiItems = db.ChungLoais.ToList();
                ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
                return View(loai);
            }
            catch(Exception ex)
            {
                object cauBaoLoi = "lưu thông tin không thành công.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
        }
        #endregion

        #region sữa
        [Route("hieu-chinh-{id}")]
        // GET: quan-ly/loai/hieu-chinh-5
        [Authorize(Roles = "NhanVien")]
        public ActionResult Edit(int? id)
        {           
            // nếu id = null trở về trang chủ
            if (id == null || id < 1) return RedirectToAction("Index");
            try
            {
                Loai loai = db.Loais.Find(id);

                if (loai == null)
                {
                    throw new Exception("Loại ID: " + id + " Không tồn tại!");
                }
                List<ChungLoai> chungLoaiItem = db.ChungLoais.ToList();
                ViewBag.ChungLoaiID = new SelectList(chungLoaiItem, "ChungLoaiID", "Ten");
                
                return View(loai);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
        }

        [Route("hieu-chinh-{id}")]
        // POST: quan-ly/loai/hieu-chinh-5
        [Authorize(Roles = "NhanVien")]
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoaiID,Ten,ChungLoaiID,BiDanh")] Loai loai)
        {
            try
            {               
                if (ModelState.IsValid)
                {                    
                    int d = db.Loais.Count(p => p.LoaiID != loai.LoaiID && p.Ten == loai.Ten);
                    if (d > 0)
                    {
                        ModelState.AddModelError("Ten", "Tên thiết bị này đã tồn tại.");
                    }
                    //trường hợp dữ liệu nhập hợp lệ(không vi phạm các kiểm tra cài đặt trong data model)
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Entry(loai).State = EntityState.Modified;
                    db.SaveChanges();
                    // lưu thành công
                    //điều hướng về action Index của controller điều hành
                    return RedirectToAction("Index");
                }
                //trường hợp dữ liệu nhập không hợp lệ
                //quay trở lại view và chuyền lại  dữ liệu
                List<ChungLoai> chungLoaiItems = db.ChungLoais.ToList();
                ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
                return View(loai);
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "cập nhật không thành công.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
        }
        #endregion

        #region xoá
        [HttpGet]
        [Route("xoa-{id?}")]
        // GET: quan-ly/loai/xoa-5
        [Authorize(Users ="admin@gmail.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null||id<1)
            {
                return RedirectToAction("Index");
            }
            try
            {
                Loai loai = db.Loais.Find(id);
                if (loai == null)
                {
                    throw new Exception("Loại id= " + id + " không tồn tại.");
                }
                return View(loai);
            }
            catch(Exception ex)
            {

                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>" + ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
        }

        [Route("xoa-{id?}")]
        // POST: quan-ly/loai/xoa-5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try {
                Loai loai = db.Loais.Find(id);
                db.Loais.Remove(loai);
                db.SaveChanges();
                //huỷ thành công, điều hướng về action index của controller hiện hành
                //--> điều hướng về trang danh sách loại
                return RedirectToAction("Index");

            }
            catch(Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.<br/>";
                int d = db.SanPhams.Count(p => p.LoaiID == id);
                if (d > 0)
                    cauBaoLoi += "Vì đã có thông tin về sản phẩm liên quan ";
                else
                    cauBaoLoi += ex.Message;
                return View("Error", cauBaoLoi);//pt6
            }
         }
        #endregion

        #region xoá biến db
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
