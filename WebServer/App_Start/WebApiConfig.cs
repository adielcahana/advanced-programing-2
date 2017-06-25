using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebServer
{
	public static class WebApiConfig
    {
		/// <summary>
		/// Registers the specified configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

	        config.Routes.MapHttpRoute(
		        name: "GetUser",
		        routeTemplate: "api/{controller}/{id}/{Password}",
		        defaults: new {controller = "User"}
	        );

	        config.Routes.MapHttpRoute(
		        name: "DefaultApi",
		        routeTemplate: "api/{controllser}/{id}",
		        defaults: new { id = RouteParameter.Optional }
	        );
		}
    }
}
