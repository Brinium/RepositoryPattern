using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repository
{ 
    public interface IRepositoryBase
    {
        IUnitOfWork UnitOfWork { get; set; }
        void AddObject(object entity);
		void UpdateObject(object entity);
    }

    public interface IRepository<T> : IRepositoryBase where T : class
	{
		IQueryable<T> All();
		IQueryable<T> Where(Expression<Func<T, bool>> expression);
		T FirstOrDefault(Expression<Func<T, bool>> expression);
		void Add(T entity);
		void Delete(T entity);
		void Delete(Expression<Func<T, bool>> expression);
		void Update(T entity);
	}
}

