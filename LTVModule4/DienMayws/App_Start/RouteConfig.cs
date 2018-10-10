using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DienMayws
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "MR2",
                url: "thong-tin-san-pham/{name}-{id}",
                defaults:new { Controller="SanPham",action="ChiTiet"});

            routes.MapRoute(
                name: "MR1",
                url: "{controller}/{action}/{name}-{id}"
              
            );

            routes.MapRoute(
                name: "MR3",
                url: "xem-thong-tin/{name}-{id}",
                defaults: new { Controller = "SanPham", action = "ChiTiet" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
