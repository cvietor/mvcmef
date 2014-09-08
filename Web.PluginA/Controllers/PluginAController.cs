using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.PluginA.Controllers
{
    [Export(typeof(IController))]
    [ExportMetadata("ControllerName", "PluginA")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PluginAController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Blablabla";
            return View();
        }
    }
}