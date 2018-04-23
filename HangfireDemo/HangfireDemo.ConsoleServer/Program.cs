using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using HangfireDemo.Web;
using Topshelf;

namespace HangfireDemo.ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseRedisStorage("hangfiredemo.redis.cache.windows.net:6380,password=Ihtp+DY31J5i5/CIC2NaZyvQz2q8e5WeCrC4fBcEMNU=,ssl=True,abortConnect=False");

            var container = MvcApplication.CreateContainer();

            GlobalConfiguration.Configuration.UseAutofacActivator(container, false);
            GlobalConfiguration.Configuration.UseBatches();

            HostFactory.Run(x =>
            {
                x.UseNLog();

                x.Service<HangfireService>(h =>
                {
                    h.ConstructUsing(n => new HangfireService());
                    h.WhenStarted(s => s.Start());
                    h.WhenStopped(s => s.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Hangfire Service");
                x.SetDisplayName("Hangfire Service");
                x.SetServiceName("Hangfire Service");
            });
        }
    }
}
