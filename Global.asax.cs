using System.Web.Optimization;
using System;
using System.Collections.Generic;
using System.Data.Entity;          // Thêm dòng này nếu có dùng DbInitializer
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TeaShopWeb.Models;           // Thêm dòng này

namespace TeaShopWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Kích hoạt tự động tạo DB + seed dữ liệu
            Database.SetInitializer(new DbInitializer());
        }
    }
}