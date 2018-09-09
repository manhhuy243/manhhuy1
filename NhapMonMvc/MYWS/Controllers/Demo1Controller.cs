using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYWS.Controllers
{
    public class Demo1Controller : Controller
    {

        #region 1 - Binding to primitive type
        //----- Truyền dữ liệu qua các tham số đơn lẻ-----

        //GET: Demo1/TinhTong
        public ViewResult TinhTong()
        {

            return View();
        }
        //Post: Demo1/TinhTong
        [HttpPost]
        public ViewResult TinhTong(int SoA,int SoB)
        {
            int kq = SoA + SoB;
            ViewBag.KetQua = string.Format("Kết quả: {0}", kq);
            return View();
        }

        #endregion

        #region 2 - Binding to Primitive type
        //------- truyền dữ liệu về qua các tham số đơn lẻ - submid multi action
        //------- truyền dữ liệu trực tiếp từ action lên view

        //Get: Demo1/PhepTinh
        public ViewResult PhepTinh()
        {
            //Chỉ định View mặc định hiển thị
            return View();
        }

        //Post: Demo1/Tong
        [HttpPost]
        public ViewResult Tong(int SoA,int SoB)
        {
            int kq = SoA + SoB;
            //Truyền dữ liệu sang view
            ViewBag.KetQua = string.Format("Kết quả: {0} + {1} = {2}", SoA, SoB, kq);
            // Chỉ định view "PhepTinh" hiển thị
            return View("PhepTinh");//pt4
        }

        //Post: Demo1/Tich
        [HttpPost]
        public ViewResult Tich(int SoA,int SoB)
        {
            int kq = SoA * SoB;
            ViewBag.KetQua = string.Format("Kết quả: {0} x {1} = {2}", SoA, SoB, kq);
            return View("PhepTinh");//pt4
        }
        #endregion

        #region 3 - Binding to Primitive type
        //------- truyền dữ liệu về qua các tham số đơn lẻ - submid multi action
        //------- TempData: Dùng để truyền dữ liệu sang view, dữ liệu được duy trì trong current request( không dữ lại dữ liệu trên textbox)

        //Get: Demo1/PhepTinh2
        public ViewResult PhepTinh2()
        {
            //Chỉ định View mặc định hiển thị
            return View();
        }

        //Post: Demo1/XuLyTinhTong
        [HttpPost]
        public ActionResult XuLyTinhTong(int SoA, int SoB)
        {
            int kq = SoA + SoB;
            //Truyền dữ liệu sang view
            TempData["KetQua"] = string.Format("Kết quả: {0} + {1} = {2}", SoA, SoB, kq);
            // Điều hướng sang action "PhepTinh2"
            return RedirectToAction("PhepTinh2"); 
        }

        //Post: Demo1/XuLyTinhTich
        [HttpPost]
        public ActionResult XuLyTinhTich(int SoA, int SoB)
        {
            int kq = SoA * SoB;
            TempData["KetQua"] = string.Format("Kết quả: {0} x {1} = {2}", SoA, SoB, kq);
            return RedirectToAction("PhepTinh2");
        }
        #endregion


    }
}