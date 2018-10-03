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
    public class DemoPageListController : Controller
    {
        DienMayDBContext db = new DienMayDBContext();
        // GET: DemoPageList
        public ActionResult Index(int? page)
        {
            var products = db.SanPhams.OrderBy(p=>p.SanPhamID).AsQueryable();

            var pageNumber = page ?? 1;
            var onePageOfProducts = products.ToPagedList(pageNumber, 25); 

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
        public ActionResult List(int? page)
        {
            if (page == null || page.Value < 1) page = 1;

            IPagedList<SanPham> onePageOfProducts = db.SanPhams
                                                      .OrderBy(p => p.SanPhamID)              
                                                      .ToPagedList(page.Value, 25);

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
    }
}