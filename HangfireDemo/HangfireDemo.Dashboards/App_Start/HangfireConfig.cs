using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Hangfire.Pro.Redis;
using Hangfire.SqlServer;
using Owin;

namespace HangfireDemo.Dashboards
{
    public class HangfireConfig
    {
        public static void Configure(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseBatches();

            var nugetDemoStorage = new SqlServerStorage("NugetDemo");

            app.UseHangfireDashboard("/nugetDemo", new DashboardOptions(), nugetDemoStorage);

            var hangfireDemoStorage =
                new RedisStorage(
                    "hangfiredemo.redis.cache.windows.net:6380,password=Ihtp+DY31J5i5/CIC2NaZyvQz2q8e5WeCrC4fBcEMNU=,ssl=True,abortConnect=False");

            app.UseHangfireDashboard("/hangfireDemo", new DashboardOptions(), hangfireDemoStorage);
        }
    }
}