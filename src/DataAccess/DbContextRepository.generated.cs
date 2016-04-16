using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class DbContextRepository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbSet<T> _dbset;
        private DbSet<T> DbSet
        {
            get
            {
                if (_dbset == null)
                {
                    _dbset = UnitOfWork.Context.Set<T>();
                }
                return _dbset;
            }
        }

        public void AddObject(object entity)
        {
            if (entity is T)
            {
                Add((T)entity);
            }
        }

        public void UpdateObject(object entity)
        {
            if (entity is T)
            {
                Update((T)entity);
            }
        }

        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

		public T FirstOrDefault(Expression<Func<T, bool>> expression)
		{
            return DbSet.FirstOrDefault(expression);
		}

        public void Add(T entity)
        {
			DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var objects = Where(expression);
            foreach (var obj in objects)
			{
                Delete(obj);
			}
		}

        public void Update(T entity)
        {
            //UnitOfWork.Context.Entry<T>(entity).State = System.Data.EntityState.Modified;
            var entry = UnitOfWork.Context.Entry<T>(entity);
            DbSet.Attach(entity);
            entry.State = System.Data.EntityState.Modified;
        }
    }
}
