using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangfireDemo.Domains;

namespace HangfireDemo.Logic.Repositories
{
    public interface IRepository<T> where T : BaseObject
    {
        IEnumerable<T> GetAll();

        void Add(T entity);

        void SaveChanges();

        T GetById(int id);
    }
}
