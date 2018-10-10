using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DienMayws.Models;
using DienMayws.ViewModels;

namespace DienMayws.Controllers
{
    public class ChungLoaiController : Controller
    {
        DienMayDBContext db = new DienMayDBContext();

        [ChildActionOnly]
        public PartialViewResult _ChungLoaiMenu1Partial()
        {
            List<ChungLoai> items = db.ChungLoais
                                    .Where(p => p.Loais.Count > 0)
                                    .Include(p => p.Loais)
                                    .ToList();
            //truyền dữ liệu sang view
            ViewBag.ChungLoais = items;

            //chỉ định Partial view mặc định hiển thị
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult _ChungLoaiMenu2Partial()
        {
            List<ChungLoaiViewModel> items = db.ChungLoais
                                      .Where(p => p.Loais.Count > 0)
                                      .Select(cl=>new ChungLoaiViewModel
                                      {
                                           ChungLoaiID= cl.ChungLoaiID,
                                           Ten = cl.Ten,
                                           BiDanh=cl.BiDanh,
                                           TongSoSanPham = cl.Loais.Sum(l=>l.SanPhams.Count),
                                           Loais = cl.Loais
                                      })
                                      .ToList();
            //truyền dữ liệu sang view
            ViewBag.ChungLoais = items;

            //chỉ định Partial view mặc định hiển thị
            return PartialView();
        }
    }
}