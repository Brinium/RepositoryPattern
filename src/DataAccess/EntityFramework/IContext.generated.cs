using System.Data.Objects;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccess.Repository.EntityFramework
{
	public interface IContext
	{
        void Dispose();
        DbSet<T> Set<T>() where T : class;
	}
}
