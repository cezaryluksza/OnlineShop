using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "", new { controller = "Product", action = "List", category = (string)null, page = 1 });

            routes.MapRoute(null, "Strona{page}", new { controller = "Product", action = "List", category = (string)null }, new { page = @"\d+" });

            routes.MapRoute(null, "{category}", new { controller = "Product", action = "List", page = 1 });

            routes.MapRoute(null, "{category}/Strona{page}", new { controller = "Product", action = "List" }, new { page = @"\d+" });

            routes.MapRoute(null, "{controller}/{action}");

            //routes.MapRoute(null, "Sortowanie{sorting}", new { controller = "Product", action = "List", sorting = UrlParameter.Optional });
        }
    }
}
