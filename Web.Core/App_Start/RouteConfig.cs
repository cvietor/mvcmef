using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebGrease.Css.Extensions;

namespace Web.Core
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "WithFriendlyName",
                "{friendlyName}/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { friendlyName = new MustBePluginName() }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }

        public class MustBePluginName : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                // return true if this is a valid friendlyName
                // MUST BE CERTAIN friendlyName DOES NOT MATCH ANY
                // CONTROLLER NAMES OR AREA NAMES
                var directories = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"), "*", SearchOption.AllDirectories);
                var plugins = directories.Select(d => Path.GetFileName(d));
                return plugins.FirstOrDefault(d => d.ToLowerInvariant() == values[parameterName].ToString().ToLowerInvariant()) != null;
            }
        }

        public class MustNotRequireFriendlyName : IRouteConstraint
        {
            private const string controllersRequiringFriendlyNames = "SignIn~SignOut~Account";

            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                // return true if this controller does NOT require a friendlyName
                return controllersRequiringFriendlyNames.ToLowerInvariant()
                    .Contains(values[parameterName].ToString().ToLowerInvariant());
            }
        }
    }
}
