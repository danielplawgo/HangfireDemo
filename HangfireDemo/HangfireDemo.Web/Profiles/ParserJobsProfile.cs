using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HangfireDemo.Domains;
using HangfireDemo.Web.ViewModels.ParserJobs;

namespace HangfireDemo.Web.Profiles
{
    public class ParserJobsProfile : Profile
    {
        public ParserJobsProfile()
        {
            CreateMap<ParserJob, IndexItemViewModel>();

            CreateMap<ParserJob, ParserJobViewModel>()
                .ReverseMap();
        }
    }
}