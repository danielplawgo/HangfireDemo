using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangfireDemo.Domains;

namespace HangfireDemo.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base ("Name=DefaultConnection")
        {
            
        }

        public DbSet<ParserJob> ParserJobs { get; set; }
    }
}
