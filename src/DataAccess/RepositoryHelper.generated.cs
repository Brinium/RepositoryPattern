using System;
using DataAccess.Entities;

namespace DataAccess.Repository
{
	public static class RepositoryHelper
	{
        public static IUnitOfWork GetUnitOfWork(string connectionString = null)
        {
            var unitOfWork = new DbContextUnitOfWork(connectionString);
            unitOfWork.TestConnectionCanOpen();
            return unitOfWork;
        }

        public static IRepositoryBase GetRepository<T>(IUnitOfWork unitOfWork) where T : class
        {
            var type = typeof(T);
            return GetRepository(type, unitOfWork);
        }

        public static IRepositoryBase GetRepository(Type type, IUnitOfWork unitOfWork)
        {
            if (type == typeof(Person))
            {
				var repository = GetPersonRepository(unitOfWork);
                return repository;
            }
            return null;
        }
		

        public static IPersonRepository GetPersonRepository()
        {
            var unitOfWork = GetUnitOfWork();
            var repository = GetPersonRepository(unitOfWork);
            return repository;
        }
		
        public static IPersonRepository GetPersonRepository(IUnitOfWork unitOfWork)
        {
            var repository = new PersonRepository();
            repository.UnitOfWork = unitOfWork;
            return repository;
        }
	
	}
}
