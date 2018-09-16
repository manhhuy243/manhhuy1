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
        // GET: Demo/TestModel
        public ViewResult TestModel()
        {
            DienMayDBContext db = new DienMayDBContext();
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