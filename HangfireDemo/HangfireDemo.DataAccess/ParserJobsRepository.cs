using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangfireDemo.Domains;
using HangfireDemo.Logic.Repositories;

namespace HangfireDemo.DataAccess
{
    public class ParserJobsRepository : BaseRepository<ParserJob>, IParserJobsRepository
    {
        public ParserJobsRepository(DataContext db) : base(db)
        {
        }

        protected override DbSet<ParserJob> DbSet
        {
            get { return Db.ParserJobs; }
        }

        public void UpdateCount(ParserJob job, int count)
        {
            Db.Database.ExecuteSqlCommand("UPDATE dbo.ParserJobs SET Count = Count + @p0 WHERE Id = @p1", count, job.Id);
        }
    }
}
