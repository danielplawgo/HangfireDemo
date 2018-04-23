using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangfireDemo.Domains
{
    public class ParserJob : BaseObject
    {
        public string Url { get; set; }

        public string Pattern { get; set; }

        public int Count { get; set; }

        public bool IsCritical { get; set; }

        public ScheduleType ScheduleType { get; set; }

        public int Depth { get; set; }

        public ParserJobStatus Status { get; set; }
    }

    public enum ScheduleType
    {
        None,
        Minutely,
        Hourly
    }

    public enum ParserJobStatus
    {
        Pending,
        Processing,
        Completed
    }
}
