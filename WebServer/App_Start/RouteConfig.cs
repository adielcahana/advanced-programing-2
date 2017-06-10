using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

	        routes.MapMvcAttributeRoutes();

//			routes.MapRoute(
//		        name: "GenerateMaze",
//		        url: "{controller}/{action}/{name}/{row}/{col}",
//		        defaults: new { controller = "SinglePlayer",action = "GenerateMaze", name = "", row=0, col=0 }
//	        );
//
//			routes.MapRoute(
//                name: "Default",
//                url: "{controller}/{action}/{id}",
//                defaults: new { action = "Index", id = UrlParameter.Optional }
//            );
		}
    }
}

