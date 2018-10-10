using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DienMayws.Models;
using PagedList;

namespace DienMayws.Controllers
{
    public class SanPhamController : Controller
    {
        DienMayDBContext db = new DienMayDBContext();

        [ChildActionOnly]//ngăn không cho bên ngoài get trực tiếp vào action
        public PartialViewResult _SanPhamMoiPartial()
        {
            // chỉ định model truy vấn dữ liệu
            List<SanPham> items = db.SanPhams
                                    .OrderByDescending(p => p.SanPhamID)
                                    .Take(10)
                                    .ToList();
            // chỉ định partialview mặc định hiển thị và chuyền dữ liệu qua model object
            return PartialView(items);//pt2
        }

        [ChildActionOnly]//ngăn không cho bên ngoài get trực tiếp vào action
        public PartialViewResult SanPhamMoi(int idLoai)
        {
            // chỉ định model truy vấn dữ liệu
            Loai loaiItem = db.Loais.Find(idLoai);
            ViewBag.TenLoai = loaiItem.Ten;
            List<SanPham> items = db.SanPhams
                                    .Where(p=>p.LoaiID==idLoai)
                                    .OrderByDescending(p => p.SanPhamID)
                                    .Take(8)
                                    .ToList();
            // chỉ định partialview "_SanPhamMoiPartial" hiển thị và chuyền dữ liệu qua model object
            return PartialView("_SanPhamMoiPartial",items);//pt4
        }

        //Get: SanPham/ChiTiet/417
        public ActionResult ChiTiet(int? id,string name)
        {
            if (id == null || id < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                SanPham item = db.SanPhams.Include(p=>p.NhaSanXuat)
                                          .SingleOrDefault(p=>p.SanPhamID==id);
                if (item == null) throw new Exception("Id=" + id + "không tồn tại");
                //chỉ định view mặc định hiển thị và truyền dữ liệu qua Model object
                return View(item);//pt3
            }
            catch (Exception ex)
            {
                object cauBaoLoi = "Lỗi truy cập dữ liệu.</br>Lý do: " + ex.Message;
                //chỉ định view "ThongBao" hiển thị và truyền dữ liệu qua model object
                return View("ThongBao",cauBaoLoi);//pt6
            }

        }

        #region sử dụng page list
        //Get: SanPham/List/
        public ViewResult List(int? page)
        {
           
            try
            {
                if (page == null || page < 1) page = 1;
                IPagedList<SanPham> items = db.SanPhams
                                        .OrderByDescending(p => p.SanPhamID)
                                        .ToPagedList(page.Value, 6);
                ViewBag.SanPhams = items;
                ViewBag.TieuDe = "Danh Sách Sản Phẩm";
                ViewBag.SanPhamActive = "active";
                //chỉ định view mặc định hiển thị
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.TieuDe = "Sản Phẩm Không Tồn Tại.";
                ViewBag.SanPhams = new List<SanPham>();
                return View();
            }
        }
        //Get: SanPham/TraCuuTheoChungLoai/3
        public ActionResult TraCuuTheoChungLoai(int? id,string name,int? page)
        {
            if (id == null || id < 1) return RedirectToAction("List");
            try
            {

                ChungLoai chungLoaiItem = db.ChungLoais.Find(id);
                if (chungLoaiItem == null) throw new Exception("Chủng Loại Id= " + id + " không tồn tại");
                if (page == null || page.Value < 1) page = 1;
                IPagedList<SanPham> sanPhamItems = db.SanPhams
                                             .Where(p => p.Loai.ChungLoaiID == id)
                                             .OrderByDescending(p => p.SanPhamID)
                                             .ToPagedList(page.Value,6);

                //truyền dữ liệu qua view
                TempData["ChungLoaiID"] = id;
                ViewBag.SanPhams = sanPhamItems;
               ViewBag.TieuDe = "Danh sách sản phẩm - " + chungLoaiItem.Ten;
            }
            catch (Exception ex)
            {
                //object cauBaoLoi = "lỗi truy cập dữ liệu.<br/>Lý Do: "+ex.Message;
                ////chỉ định view "ThongBao" hiển thị và truyền câu báo lỗi sang 
                //return View("ThongBao", cauBaoLoi);//pt6
                ViewBag.TieuDe = "Sản Phẩm Không Tồn Tại.";
                ViewBag.SanPhams = new List<SanPham>();
            }
            //Chỉ Định view "List" hiển thị     
            return View("List");
        }
        public ActionResult TraCuuTheoLoai(int? id,string name, int? page)
        {
            if (id == null || id < 1) return RedirectToAction("List");
            try
            {

                Loai loaiItem = db.Loais.Find(id);
                if (loaiItem == null) throw new Exception("Loại Id= " + id + " không tồn tại");
                if (page == null || page.Value < 1) page = 1;
                IPagedList<SanPham> sanPhamItems = db.SanPhams
                                                     .Where(p => p.Loai.LoaiID == id)
                                                     .OrderByDescending(p => p.SanPhamID)
                                                     .ToPagedList(page.Value, 6);

                //truyền dữ liệu qua view
                TempData["LoaiID"] = id;
                ViewBag.SanPhams = sanPhamItems;
               ViewBag.TieuDe = "Danh sách sản phẩm - " + loaiItem.Ten;
            }
            catch (Exception ex)
            {
                //object cauBaoLoi = "lỗi truy cập dữ liệu.<br/>Lý Do: "+ex.Message;
                ////chỉ định view "ThongBao" hiển thị và truyền câu báo lỗi sang 
                //return View("ThongBao", cauBaoLoi);//pt6
                ViewBag.TieuDe = "Sản Phẩm Không Tồn Tại.";
                ViewBag.SanPhams = new List<SanPham>();
            }
            //Chỉ Định view "List" hiển thị     
            return View("List");
        }
        #endregion
        public ActionResult TraCuuTheoTen( string NoiDungTim)
        {
            var items = db.SanPhams
                        .Where(p => p.Ten.Contains(NoiDungTim))
                        .ToList();
            ViewBag.SanPhams = items;
            return View("KetQuaTim");
        }

        }
}