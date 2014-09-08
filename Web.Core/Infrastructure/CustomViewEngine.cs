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
            this.ViewLocationFormats = new string[]
            {
                "~/Plugins/Web.PluginA.zip/Views/{0}.cshtml", 
            };
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var x = true;
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}