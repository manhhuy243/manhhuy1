using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYWS.Models;

namespace MYWS.Controllers
{
    public class MonHocController : Controller
    {
        // GET: MonHoc/ChiTiet/3
        public ViewResult ChiTiet(int id)
        {
            //chỉ định models truy vấn dữ liệu
            QLMonHoc qlmh = new QLMonHoc();
            MonHoc item = qlmh.Doc(id);
            //chỉ định view mặc định hiển thị và truyền dữ liệu qua Models object
            return View(item);//pt3
        }
        // GET: MonHoc/DanhSach
        public ViewResult DanhSach()
        {
            //chỉ định models truy vấn dữ liệu
            QLMonHoc qlmh = new QLMonHoc();
            List<MonHoc> items = qlmh.Doc();
            //chỉ định view mặc định hiển thị và truyền dữ liệu qua Models object
            return View(items);//pt3
        }
    }
}