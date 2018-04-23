using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using AutoMapper;

namespace HangfireDemo.Web.Infrastructure
{
    public class AutomapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var profiles = GetType().Assembly.GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .Select(t => (Profile)Activator.CreateInstance(t));

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }
    }
}