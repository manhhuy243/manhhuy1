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


    }
}