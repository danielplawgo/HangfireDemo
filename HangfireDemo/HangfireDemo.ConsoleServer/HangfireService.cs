using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using HangfireDemo.Logic;

namespace HangfireDemo.ConsoleServer
{
    public class HangfireService
    {
        private BackgroundJobServer _server;

        public void Start()
        {
            var options = new BackgroundJobServerOptions()
            {
                Queues = new[] { JobsQueues.Critical, JobsQueues.Default },
                //WorkerCount = 50
            };

            _server = new BackgroundJobServer(options);
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}
