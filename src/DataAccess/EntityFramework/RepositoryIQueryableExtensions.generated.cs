using System.Data.Objects;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace DataAccess.Repository.EntityFramework
{
	public static class RepositoryIQueryableExtensions
	{
		public static IQueryable<T> Include<T>(this IQueryable<T> source, string path)
		{
            var dbQuery = source as DbQuery<T>;
            if (dbQuery != null)
            {
                return dbQuery.Include(path);
            }
            else
            {
                var objectQuery = source as ObjectQuery<T>;
                if (objectQuery != null)
                {
                    return objectQuery.Include(path);
                }
            }
            return source;
		}
	}
}
