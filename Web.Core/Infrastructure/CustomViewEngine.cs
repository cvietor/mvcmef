using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Core.Infrastructure
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            var pluginViewLocations = new List<string>()
            {
                "~/Plugins/Web.PluginA/Views/{1}/{0}.cshtml",
                "~/Plugins/Web.PluginB/Views/{1}/{0}.cshtml"
            };

            pluginViewLocations.AddRange(base.ViewLocationFormats);
            this.ViewLocationFormats = pluginViewLocations.ToArray();
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var x = true;
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var y = true;
            return base.CreateView(controllerContext, viewPath, masterPath);
        }
    }
}