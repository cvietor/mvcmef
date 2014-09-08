using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Web.PluginB.Controllers
{
    [Export(typeof(IController))]
    [ExportMetadata("ControllerName", "PluginB")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PluginBController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "I am plugin B";

            return View();
        }
    }
}