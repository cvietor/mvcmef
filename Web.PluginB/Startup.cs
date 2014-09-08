using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.PluginB.Startup))]
namespace Web.PluginB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
