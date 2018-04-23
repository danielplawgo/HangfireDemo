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
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseObject
    {
        protected DataContext Db { get; set; }

        protected abstract DbSet<T> DbSet { get; }

        public BaseRepository(DataContext db)
        {
            Db = db;
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void SaveChanges()
        {
            Db.SaveChanges();   
        }

        public T GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }
    }
}
