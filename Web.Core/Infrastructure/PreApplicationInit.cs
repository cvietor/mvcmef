using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;

[assembly: PreApplicationStartMethod(typeof(Web.Core.Infrastructure.PreApplicationInit), "Initialize")]
namespace Web.Core.Infrastructure
{

    /// <summary>
    /// The Application Preinitalization. Thanks to Shannon Deminick (http://shazwazza.com/post/Developing-a-plugin-framework-in-ASPNET-with-medium-trust.aspx)
    /// </summary>
    public class PreApplicationInit
    {
        /// <summary>
        /// The source plugin folder from which to shadow copy from
        /// </summary>
        /// <remarks>
        /// This folder can contain sub folderst to organize plugin types
        /// </remarks>
        private static readonly DirectoryInfo PluginFolder;

        /// <summary>
        /// The folder to shadow copy the plugin DLLs to use for running the app
        /// </summary>
        private static readonly DirectoryInfo ShadowCopyFolder;

        static PreApplicationInit()
        {
            PluginFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/plugins"));
            ShadowCopyFolder = new DirectoryInfo(HostingEnvironment.MapPath("~/plugins/temp"));
        }

        public static void Initialize()
        {
            ShadowCopyPlugins();
        }

        private static void ShadowCopyPlugins()
        {
            // create shadow copy directory, if not exists already
            Directory.CreateDirectory(ShadowCopyFolder.FullName);

            //clear all plugins from shadow copy folder
            foreach (var file in ShadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                file.Delete();
            }

            // shadow copy files
            foreach (var pluginFile in PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                var path = Path.Combine(ShadowCopyFolder.FullName, pluginFile.Directory.Name);
                var directory = Directory.CreateDirectory(path);
                File.Copy(pluginFile.FullName, Path.Combine(directory.FullName, pluginFile.Name), true);
            }

        }
    }
}