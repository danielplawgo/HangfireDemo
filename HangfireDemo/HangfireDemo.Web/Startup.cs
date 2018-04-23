using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HangfireDemo.Web.Startup))]
namespace HangfireDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            HangfireConfig.Configure(app);
        }
    }
}
