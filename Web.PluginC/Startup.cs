using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.PluginC.Startup))]
namespace Web.PluginC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
