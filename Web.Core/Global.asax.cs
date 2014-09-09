using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

using Web.Core.Infrastructure;

namespace Web.Core
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var pluginDirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins", "temp");
            ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(pluginDirPath));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomViewEngine());
        }
    }
}
