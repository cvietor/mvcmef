using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;

using System.Web;
using System.Web.Mvc;
using Utils;

namespace Web.Administration.Controllers
{
    public class PluginController : Controller
    {
        // GET: Plugin
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult CreatePackage()
        {
            return View();
        }

        public ViewResult ActivatePackages()
        {
            var rawFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PluginPackages"));
            var files = rawFiles.Select(file => Path.GetFileName(file)).ToList();

            return View(files);
        }

        [HttpPost]
        public ActionResult CreatePackage(IEnumerable<HttpPostedFileBase> files)
        {
            string packageName = Path.GetDirectoryName(files.FirstOrDefault().FileName).Split(Path.PathSeparator).FirstOrDefault();

            string zipContainerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PluginPackages", string.Format("{0}_{1}.zip", packageName, DateTime.Now.Ticks));
            foreach (var file in files)
            {
                //TODO: file names with spaces resulting in %20 files
                string fileName = file.FileName.Replace(packageName + "/", string.Empty).Replace("/", Path.DirectorySeparatorChar.ToString());
                ZipUtil.AddFileToZip(zipContainerPath, fileName, file.InputStream);
            }
            
            return View();
        }

        
    }
}