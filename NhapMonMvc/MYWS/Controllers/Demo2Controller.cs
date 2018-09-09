using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYWS.Models;


namespace MYWS.Controllers
{
    public class Demo2Controller : Controller
    {
        #region 3 - Binding to Primitive type
        //------- truyền dữ liệu về qua các tham số đơn lẻ - submid multi action
        //------- TempData: Dùng để truyền dữ liệu sang view, dữ liệu được duy trì trong current request( không dữ lại dữ liệu trên textbox)

        //Get: Demo2/PhepTinh2
        public ViewResult PhepTinh2()
        {
            //Chỉ định View mặc định hiển thị
            return View();
        }

        //Post: Demo2/XuLyTinhTong
        [HttpPost]
        public ActionResult XuLyTinhTong(SoNguyenModel entity)
        {
            if (ModelState.IsValid)
            {
                int kq = entity.SoA + entity.SoB + entity.SoC;
                //Truyền dữ liệu sang view
                TempData["KetQua"] = string.Format("Kết quả: {0} + {1} + {2} = {3}", entity.SoA, entity.SoB, entity.SoC, kq);
                // Điều hướng sang action "PhepTinh2"
                return RedirectToAction("PhepTinh2");
            }
            return View("PhepTinh2", entity);
        }

        //Post: Demo2/XuLyTinhTich
        [HttpPost]
        public ActionResult XuLyTinhTich(SoNguyenModel entity)
        {
            if (ModelState.IsValid)
            {
                int kq = entity.SoA * entity.SoB * entity.SoC;
                TempData["KetQua"] = string.Format("Kết quả: {0} x {1} x {2} = {3}", entity.SoA, entity.SoB, entity.SoC,kq);
                return RedirectToAction("PhepTinh2");
            }
            return View("PhepTinh2", entity);
         }
        #endregion
    }
}