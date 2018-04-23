using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using HangfireDemo.Logic;
using Owin;

namespace HangfireDemo.Web
{
    public class HangfireConfig
    {
        public static void Configure(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseRedisStorage("hangfiredemo.redis.cache.windows.net:6380,password=Ihtp+DY31J5i5/CIC2NaZyvQz2q8e5WeCrC4fBcEMNU=,ssl=True,abortConnect=False");
            GlobalConfiguration.Configuration.UseBatches();

            var options = new BackgroundJobServerOptions()
            {
                Queues = new [] { JobsQueues.Critical, JobsQueues.Default },
                //WorkerCount = 50
            };

            app.UseHangfireServer(options);
        }
    }
}