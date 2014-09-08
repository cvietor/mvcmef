using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.Administration.Startup))]
namespace Web.Administration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
