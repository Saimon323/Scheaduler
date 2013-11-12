using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Model.Repositories.Interfaces
{
    public interface IBaseRepository<T>
        where T : class, IIdentifable
    {
        void Add(T item);

        void Delete(T item);


        T GetById(int id);

        IList<T> GetAll();

        IQueryable<T> Query(Expression<Func<T, bool>> query);


        void Save();

        void Dispose();

        void Refresh(T item);
    }
}
