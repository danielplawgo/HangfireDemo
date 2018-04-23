using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HangfireDemo.Domains;

namespace HangfireDemo.Web.ViewModels.ParserJobs
{
    public class ParserJobViewModel
    {
        public ParserJobViewModel()
        {
            ScheduleTypes = new List<SelectListItem>()
            {
                new SelectListItem() {Text = ScheduleType.None.ToString(), Value = ScheduleType.None.ToString()},
                new SelectListItem() {Text = ScheduleType.Minutely.ToString(), Value = ScheduleType.Minutely.ToString()},
                new SelectListItem() {Text = ScheduleType.Hourly.ToString(), Value = ScheduleType.Hourly.ToString()},
            };
        }

        public int Id { get; set; }

        public string Url { get; set; }

        public string Pattern { get; set; }

        public int Count { get; set; }

        public bool IsCritical { get; set; }

        public ScheduleType ScheduleType { get; set; }

        public IEnumerable<SelectListItem> ScheduleTypes { get; set; }

        public int Depth { get; set; }
    }
}