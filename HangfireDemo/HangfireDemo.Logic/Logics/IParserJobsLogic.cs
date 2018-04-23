using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangfireDemo.Domains;

namespace HangfireDemo.Logic.Logics
{
    public interface IParserJobsLogic
    {
        IEnumerable<ParserJob> GetAll();

        void Add(ParserJob job);

        void ParseUrl(int id);

        void ParseCriticalUrl(int id);
    }
}
