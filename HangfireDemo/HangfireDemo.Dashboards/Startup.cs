using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HangfireDemo.Dashboards.Startup))]
namespace HangfireDemo.Dashboards
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
