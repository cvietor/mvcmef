using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.Core.Startup))]
namespace Web.Core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
