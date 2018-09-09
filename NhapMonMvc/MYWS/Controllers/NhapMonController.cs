using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYWS.Controllers
{
    public class NhapMonController : Controller
    {
        #region phần 1
        // GET: NhapMon/Chao
        public string Chao()
        {
            string noiDung = "xin chào";
            return noiDung;
            
        }
        // GET: NhapMon/Chaomung/uyen
        public string Chaomung(string id)
        {
            string noiDung = "xin chào "+id;
            return noiDung;

        }
        // GET: NhapMon/Chaomung2?name=uyen
        public string Chaomung2(string name)
        {
            string noiDung = "xin chào " + name;
            return noiDung;

        }
        #endregion


        //GET: NhapMon/ThongTinHocVien
        public ViewResult ThongTinHocVien()
        {
            //chỉ định view mặc định hiển thị
            return View();//pt1 không tham số
        }
        //GET: NhapMon/GioiThieu
        public ViewResult GioiThieu()
        {
            //chỉ định view 'ThongTinHocVien' hiển thị
            return View("ThongTinKhachHang");//pt4 có tham số
        }
    }
}