using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repository.DapperContrib
{
    public class ReposEFContext : DbContext
    {
        public ReposEFContext()
        {
        }

        public ReposEFContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Person> ApplicationSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
