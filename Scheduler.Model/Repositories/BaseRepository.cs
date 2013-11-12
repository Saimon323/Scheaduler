using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Scheduler.Model.Repositories.Interfaces;

namespace Scheduler.Model.Repositories
{
    public abstract class BaseRepository<T, TEntity> : IBaseRepository<T>
        where T : class, IIdentifable
        where TEntity : ObjectContext, new()
    {
        private readonly TEntity _entities;

        protected BaseRepository()
        {
            //int? costam = new TEntity().CommandTimeout;
            _entities = new TEntity();
        }

        protected BaseRepository(TEntity entities)
            : this(entities, false)
        {
        }

        protected BaseRepository(TEntity entities, bool refreshObjects)
        {
            _entities = entities;

            if (refreshObjects)
                ((ObjectStateManager)(Entities.GetType().GetProperty("ObjectStateManager").GetValue(Entities, null))).
                    GetObjectStateEntries(EntityState.Unchanged).All(ose =>
                    {
                        if (ose.Entity != null)
                            (Entities.GetType().GetMethod("Refresh"))
                                .Invoke(Entities, null);
                        //((ObjectContext)Entities).Refresh(RefreshMode.StoreWins, ose.Entity);
                        return true;
                    });
        }

        public TEntity Entities
        {
            get { return _entities; }
        }

        public abstract IQueryable<T> Items { get; }

        #region IBaseRepository<T> Members

        /// <summary>
        ///     Pobierz przez identyfikator
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return Items.Where(i => i.id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        ///     Pobierz wszystkie
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAll()
        {
            return Items.ToList();
        }

        /// <summary>
        ///     Pobierz przez zapytanie
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> query)
        {
            return Items.Where(query);
        }

        /// <summary>
        ///     Dodaj
        /// </summary>
        /// <param name="item"></param>
        public abstract void Add(T item);

        /// <summary>
        ///     Usuń
        /// </summary>
        /// <param name="item"></param>
        public void Delete(T item)
        {
            _entities.DeleteObject(item);
        }

        /// <summary>
        ///     Zapisz do bazy
        /// </summary>
        public void Save()
        {
            _entities.SaveChanges();
        }

        /// <summary>
        ///     Usuń obiekt
        /// </summary>
        public void Dispose()
        {
            if (_entities != null)
            {
                _entities.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Refresh
        /// </summary>
        /// <param name="item"></param>
        public void Refresh(T item)
        {
            _entities.Refresh(RefreshMode.ClientWins, item);
        }

        #endregion
    }
}
