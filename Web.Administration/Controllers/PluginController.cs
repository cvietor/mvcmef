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
            return View();
        }

        public JsonResult GetPluginPackageNames()
        {
            string pluginPackagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PluginPackages");
            var rawFiles = Directory.GetFiles(pluginPackagesDirectory);
            var files = rawFiles.Select(file => Path.GetFileName(file)).ToList();

            return Json(files, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void ActivatePackages(string file)
        {
            var x = file;
            var pluginFullPath = @"C:\Users\cv230985\Downloads\MvcMefDemo\Web.Administration\PluginPackages\" + file;
            var solutionDir = @"C:\Users\cv230985\Downloads\MvcMefDemo\";
            var coreDir = @"C:\Users\cv230985\Downloads\MvcMefDemo\Web.Core\Plugins\";
            var targetPath = coreDir + file;

            ZipUtil.DecompressFile(pluginFullPath, targetPath);
        }

        [HttpPost]
        public ActionResult CreatePackage(IEnumerable<HttpPostedFileBase> files)
        {
            string packageName = Path.GetDirectoryName(files.FirstOrDefault().FileName).Split(Path.PathSeparator).FirstOrDefault();

            string zipContainerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PluginPackages", string.Format("{0}.zip", packageName));
            foreach (var file in files)
            {
                //TODO: file names with spaces resulting in %20 fragments
                string fileName = file.FileName.Replace(packageName + "/", string.Empty).Replace("/", Path.DirectorySeparatorChar.ToString());
                ZipUtil.AddFileToZip(zipContainerPath, fileName, file.InputStream);
            }
            
            return View();
        }

        
    }
}