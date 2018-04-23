using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangfireDemo.Domains;

namespace HangfireDemo.Logic.Repositories
{
    public interface IParserJobsRepository : IRepository<ParserJob>
    {
        void UpdateCount(ParserJob job, int count);
    }
}
