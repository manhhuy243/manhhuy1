using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DienMayws.Models;

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
        public ActionResult ChiTiet(int? id)
        {
            if (id == null || id < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                SanPham item = db.SanPhams.Find(id);
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
    }
}