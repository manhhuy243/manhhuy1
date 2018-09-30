using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;

namespace DienMayws.Controllers
{
    public class DemoController : Controller
    {

        DienMayDBContext db = new DienMayDBContext();

        public ViewResult EXDisplay()
        {
            HoaDon item = db.HoaDons.FirstOrDefault();
            return View(item);

        }
        // GET: Demo/TestModel
        public ViewResult TestModel()
        {

            List<Loai> items = db.Loais.ToList();
            return View();
        }
        //Get: Demo/TestDienMayLayout
        public ViewResult TestDienMayLayout()
        {
            return View();
        }

    }
}