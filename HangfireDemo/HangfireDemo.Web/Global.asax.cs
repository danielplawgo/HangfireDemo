using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Hangfire;
using HangfireDemo.DataAccess;
using HangfireDemo.Logic;
using HangfireDemo.Logic.ParserJobs;
using HangfireDemo.Logic.Repositories;
using HangfireDemo.Logic.Services.HtmlService;

namespace HangfireDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IContainer Container { get; set; }

        protected void Application_Start()
        {
            ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureContainer()
        {
            Container = CreateContainer();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            GlobalConfiguration.Configuration.UseAutofacActivator(Container, false);
        }

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(BaseLogic).Assembly, typeof(DataContext).Assembly)
                .AsImplementedInterfaces();
            builder.RegisterType<DataContext>();
            builder.RegisterType<ParserJobsLogic>();
            builder.RegisterType<BackgroundJobClient>().As<IBackgroundJobClient>().SingleInstance();
            builder.RegisterType<BatchJobClient>().As<IBatchJobClient>().SingleInstance();
            return builder.Build();
        }
    }
}
