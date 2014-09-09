using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Glimpse.Core.Extensions;

namespace Web.Core.Infrastructure
{
    public class CustomViewEngine : RazorViewEngine
    {
        private readonly string[] _defaultViewLocationFormats;


        public CustomViewEngine()
        {
            _defaultViewLocationFormats = ViewLocationFormats;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            ViewLocationFormats = _defaultViewLocationFormats;
            
            if (controllerContext.RouteData.Values.ContainsKey("pluginname"))
            {
                ViewLocationFormats = new string[1];
                ViewLocationFormats[0] = "~/Plugins/" + controllerContext.RouteData.Values["pluginname"] + "/Views/{1}/{0}.cshtml";
            }
           
            ViewEngineResult result = base.FindView(controllerContext, viewName, masterName, useCache);
            return result;
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {

            var view = new RazorView(controllerContext, 
                viewPath,
                layoutPath: masterPath, 
                runViewStartPages: true, 
                viewStartFileExtensions: 
                FileExtensions, 
                viewPageActivator: ViewPageActivator);

            return view;

            //return base.CreateView(controllerContext, viewPath, masterPath);
        }
    }
}