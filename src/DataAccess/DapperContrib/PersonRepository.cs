using System.Linq;
using System.Linq.Expressions;
using DataAccess.Entities;

namespace DataAccess.Repository.DapperContrib
{   
	public partial interface IPersonRepository : IRepository<Person>
	{
		Person GetById(int id);
	}
	
	public partial class PersonRepository : DbContextRepository<Person>, IPersonRepository
	{
        public Person GetById(int id)
        {
            return FirstOrDefault(x => x.Id == id);
        }
	}
}
