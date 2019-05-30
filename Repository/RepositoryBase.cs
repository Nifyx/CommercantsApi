using CommercantsAPI.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CommercantsAPI.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected Context Context { get; set; }

        public RepositoryBase(Context context)
        {
            this.Context = context;
        }

        public void Create(T entity)
        {
            this.Context.Set<T>().Add(entity);
            this.Save();
        }

        public void Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
            this.Save();
        }

        public IEnumerable<T> FindAll()
        {
            return this.Context.Set<T>();
        }

        public T Find(long id)
        {
            return this.Context.Set<T>().Find(id);
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.Context.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            this.Context.Set<T>().Update(entity);
            this.Save();
        }

        public void Save()
        {
            this.Context.SaveChanges();
        }
    }
}
