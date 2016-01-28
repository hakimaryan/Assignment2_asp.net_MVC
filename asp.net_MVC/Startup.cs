using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(asp.net_MVC.Startup))]
namespace asp.net_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
