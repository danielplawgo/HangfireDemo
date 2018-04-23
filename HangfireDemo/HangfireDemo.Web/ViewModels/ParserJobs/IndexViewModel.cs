using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangfireDemo.Web.ViewModels.ParserJobs
{
    public class IndexViewModel
    {
        public IEnumerable<IndexItemViewModel> Items { get; set; }
    }

    public class IndexItemViewModel
    {
        public string Url { get; set; }

        public string Pattern { get; set; }

        public int Count { get; set; }

        public bool IsCritical { get; set; }

        public string ScheduleType { get; set; }

        public int Depth { get; set; }

        public string Status { get; set; }
    }
}