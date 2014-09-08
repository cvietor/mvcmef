using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Web.Core.Infrastructure
{
    public class MefControllerFactory : IControllerFactory
    {
        private string pluginPath;
        private AggregateCatalog catalog;
        private CompositionContainer container;

        private DefaultControllerFactory defaultControllerFactory;

        public MefControllerFactory(string pluginPath)
        {
            this.pluginPath = pluginPath;
            this.catalog = GetAggregateCatalog(pluginPath);
            this.container = new CompositionContainer(catalog);

            this.defaultControllerFactory = new DefaultControllerFactory();

        }

        private AggregateCatalog GetAggregateCatalog(string pluginPath)
        {
            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(new DirectoryCatalog(pluginPath));

            var directories = Directory.GetDirectories(pluginPath, "*", SearchOption.AllDirectories);
            foreach (var directory in directories)
            {
                aggregateCatalog.Catalogs.Add(new DirectoryCatalog(directory));
            }

            return aggregateCatalog;
        }


        #region IControllerFactory Members

        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            IController controller = null;

            if (controllerName != null)
            {
                Lazy<IController> export = this.container.GetExports<IController, IDictionary<string, object>>()
                    .FirstOrDefault(
                        c => c.Metadata.ContainsKey("ControllerName") && 
                            c.Metadata["ControllerName"].ToString().ToLowerInvariant().Equals(controllerName.ToLowerInvariant()));

                if (export != null) {
                    controller = export.Value;
                }

            }

            if (controller == null)
            {
                return this.defaultControllerFactory.CreateController(requestContext, controllerName);
            }

            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        #endregion
    }
}
