using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
//-------------------------------------------

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework; // use for RoleManager
using Microsoft.AspNet.Identity.Owin;
using DienMayws.Models;

namespace DienMayws.Controllers
{
    
    public class DemoIdentityController : Controller
    {
        // GET: DemoIdentity/Index
        public ActionResult Index()
        {
            // 1- Tạo Role - Nhóm

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //if (!roleManager.RoleExists("QuanLy"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "QuanLy";
            //    roleManager.Create(role);
            //}
            //if (!roleManager.RoleExists("NhanVien"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "NhanVien";
            //    roleManager.Create(role);
            //}
            //-----------------------------
            // 2- Add user vào role
            // Cách 1:
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            // Cách 2:
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var userAdmin = userManager.FindByName("admin@gmail.com");
            userManager.AddToRole(userAdmin.Id, "QuanLy");
            userManager.AddToRole(userAdmin.Id, "NhanVien");

            var userHanh = userManager.FindByName("hanh@gmail.com");
            string userId = userHanh.Id;
            string roleName = "QuanLy";
            var idResult = userManager.AddToRole(userId, roleName);
            bool kq = idResult.Succeeded;

            var userHung = userManager.FindByName("hung@gmail.com");
            userManager.AddToRole(userHung.Id, "QuanLy");

            var userTam = userManager.FindByName("tam@gmail.com");
            userManager.AddToRole(userTam.Id, "NhanVien");



            //-------------------------------------------------------------------

            // 3- Kiễm tra người dùng có đăng nhập chưa 
            //bool kqKT = User.Identity.IsAuthenticated;
            //bool kqKT2 = Request.IsAuthenticated;

            //if (kqKT == true) //Nếu có đăng nhập
            //{
            //    // Lấy UserName của user hiện hành
            //    string name = User.Identity.GetUserName();
            //    // Lấy id của user hiện hành
            //    string id = User.Identity.GetUserId();
            //    // Kiễm tra user hiện hành có trong Role nhanvien không
            //    bool isInRole = User.IsInRole("NhanVien"); 

            //}

            //---------------------------------------------------------------------------------------------------------------
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            //var users = userManager.Users;

            //  Lấy thông tin của 1 user từ database khi biết userName
            //var userBinh = users.SingleOrDefault(p => p.UserName == "binh@gmail.com");

            // Đọc danh sách tất cả user từ database (table AspNetUser)
            //var userList = users.ToList();
            //  Lấy thông tin của 1 user từ userList khi biết userName
            //var userHanh = userList.Find(p => p.UserName == "hanh@gmail.com");
            //string hoTen = userHanh.LastName + " " + userHanh.FirstName;




            return View();
        }

       

    }
}




//

//    https://msdn.microsoft.com/en-us/library/microsoft.aspnet.identity(v=vs.108).aspx
//https://msdn.microsoft.com/enus/library/microsoft.aspnet.identity.usermanagerextensions(v=vs.108).aspx

//http://www.asp.net/identity
//https://msdn.microsoft.com/en-us/library/tw292whz.aspx
//https://msdn.microsoft.com/en-us/library/dn613059%28v=vs.108%29.aspx

// http://www.codeproject.com/Articles/682113/Extending-Identity-Accounts-and-Implementing-Rol
