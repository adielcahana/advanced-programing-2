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
        }
    }
}
