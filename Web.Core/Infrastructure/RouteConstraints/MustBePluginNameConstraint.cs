using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Web.Core.Infrastructure.RouteConstraints
{
    /// <summary>
    /// Thanks to counsellorben (http://stackoverflow.com/questions/8328951/alter-mvc-routing-with-dynamic-prefix-while-maintaining-backwards-url-compatibil)
    /// </summary>
    public class MustBePluginNameConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // returns true, if value of parameterName "pluginName" has a matching assembly file in the plugins folder
            var directories = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"), "*", SearchOption.AllDirectories);
            var plugins = directories.Select(d => Path.GetFileName(d));
            return plugins.FirstOrDefault(d => d.ToLowerInvariant() == values[parameterName].ToString().ToLowerInvariant()) != null;
        }
    }
}