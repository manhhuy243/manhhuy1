using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DienMayws.ViewModels;
using DienMayws.Models;
namespace DienMayws.Controllers
{
    public class GioHangController : Controller
    {
        DienMayDBContext db = new DienMayDBContext();
        // GET: GioHang
        public ActionResult Index()
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if(gioHang==null || gioHang.TongSanPham==0)
            {// Điều hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }
            return View(gioHang);
        }

        [HttpPost]
        public ActionResult Them(int sanPhamID,int soLuong=1)
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if(gioHang==null)
            {
                gioHang = new GioHangModel();
                Session["GioHang"] = gioHang;
            }
            var sanPhamChonMua = db.SanPhams.Find(sanPhamID);
            var item = new GioHangItem(sanPhamChonMua, soLuong);
            gioHang.Add(item);

            //return RedirectToAction("Index");
            string returnUrl = Request.UrlReferrer.AbsoluteUri;
            return Redirect(returnUrl);
        }
        [HttpPost]
        public ActionResult HieuChinh(int sanPhamID, int soLuong)
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Update(sanPhamID, soLuong);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Xoa(int sanPhamID)
        {
            //Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Remove(sanPhamID);
            return RedirectToAction("Index");

        }
        #region Xử lý phát sinh đơn đặt hàng

        [HttpGet]
        public ActionResult DatHang()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatHang(HoaDon hoaDon)
        {
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null || gioHang.TongSanPham == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            // Xử lý phát sinh HoaDon và HoaDonChiTiet
            try
            {
                //1. Thêm HoaDon
                hoaDon.NgayDatHang = DateTime.Now;
                hoaDon.TongTien = gioHang.TongTriGia;
                db.HoaDons.Add(hoaDon);
                //2. Thêm DatHangCT              
                foreach (var item in gioHang.Items)
                {
                    HoaDonChiTiet ct = new HoaDonChiTiet();
                    ct.HoaDonID = hoaDon.HoaDonID;
                    ct.SanPhamID = item.SanPham.SanPhamID;
                    ct.SoLuong = item.SoLuong;
                    ct.DonGia = item.SanPham.GiaBan;
                    ct.ThanhTien = item.SanPham.GiaBan * item.SoLuong;
                    db.HoaDonChiTiets.Add(ct);
                }
                db.SaveChanges();
                gioHang.Clear();

                return View("DatHangThanhCong", hoaDon);
            }
            catch (Exception ex)
            {
                ViewData["LoiDatHang"] = "Đặt hàng không thành công.<br>" + ex.Message;
                return View(hoaDon);
            }
        }
        #endregion
    }
}