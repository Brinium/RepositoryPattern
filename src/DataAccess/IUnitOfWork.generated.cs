using System.Data.Objects;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataAccess.Repository
{
	public interface IUnitOfWork
	{
		//IContext Context { get; }
        string ConnectionString { get; set; }
        bool LazyLoadingEnabled { get; set; }
        bool ProxyCreationEnabled { get; set; }
		void TestConnectionCanOpen();
		void SaveChanges();
		void DetachDatabase();
		bool IsDatabaseSuspended();
		void Dispose();
	}
}
