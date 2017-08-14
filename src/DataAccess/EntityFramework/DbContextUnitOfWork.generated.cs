using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Threading;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccess;

namespace DataAccess.Repository.EntityFramework
{
	public class DbContextUnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; set; }

		public string ConnectionString
		{
			get { return Context.Database.Connection.ConnectionString; }
			set { Context.Database.Connection.ConnectionString = value; }
		}
		
		public bool LazyLoadingEnabled { get; set; }

        public bool ProxyCreationEnabled { get; set; }

		public DbContextUnitOfWork(string connectionString = null)
		{
            if (String.IsNullOrWhiteSpace(connectionString))
                Context = new ReposEFContext();
            else
    			Context = new ReposEFContext(connectionString);
		}

        /// <summary>
        /// Opens and Closes the connection.
        /// Will throw an Exception if it cannot open the connection.
        /// Will try up to 3 times.
        /// </summary>
        /// <exception cref="EntityException">Thrown when the connection cannot open</exception>
        /// <exception cref="SqlException">Thrown when the connection cannot open</exception>
        public void TestConnectionCanOpen()
        {
            var opened = false;
            var retry = true;
            var count = 0;
            while (retry && count < 5)
            {
                try
                {
                    Context.Database.Connection.Open();
                    retry = false;
                    opened = true;
                    Context.Database.Connection.Close();
                }
                catch
                {
                    retry = true;
                    Thread.Sleep(5000);
                    count++;
                }
            }

            if (!opened)
            {
                try
                {
                    Context.Database.Connection.Open();
                    Context.Database.Connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

		public void SaveChanges()
		{
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("The records you attempted to edit or delete"
                                               + " were modified by another user after you got the original value. The"
                                               + " operation was canceled.", ex);
            }
            catch (DataException ex)
            {
                throw new DataException("Unable to save changes.", ex);
            }
		}
		
        public void DetachDatabase()
        {
            EntityConnectionStringBuilder efConnString = new EntityConnectionStringBuilder(ConnectionString);
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder(efConnString.ProviderConnectionString);
			
            var dbName = String.IsNullOrEmpty(connString.InitialCatalog) ? connString.AttachDBFilename : connString.InitialCatalog;
            var query = string.Format("USE [master]{0}ALTER DATABASE [{1}] SET OFFLINE WITH ROLLBACK IMMEDIATE{0}USE [master]{0}EXEC master.dbo.sp_detach_db @dbname = N'{1}'", Environment.NewLine, dbName);
            Context.Database.ExecuteSqlCommand(query);
        }

        public bool IsDatabaseSuspended()
        {
            EntityConnectionStringBuilder efConnString = new EntityConnectionStringBuilder(ConnectionString);
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder(efConnString.ProviderConnectionString);

            var dbName = String.IsNullOrEmpty(connString.InitialCatalog) ? connString.AttachDBFilename : connString.InitialCatalog;
            //var retVal = Context.ExecuteStoreQuery<bool>(string.Format("IF EXISTS(SELECT * FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID(N'tempdb..#WhoStatusTemp')){0}BEGIN{0}DROP TABLE #WhoStatusTemp{0}END{0}CREATE TABLE #WhoStatusTemp(spid smallint,ecid smallint,[status] nchar(30),loginame nchar(128),hostname nchar(128),blk char(5),dbname nchar(128),cmd nchar(16),request_id int){0}INSERT #WhoStatusTemp EXEC master.sys.sp_who 'active';{0}DECLARE @status bit;{0}SET @status = 0;{0}IF EXISTS(SELECT * FROM #WhoStatusTemp WHERE [status] = 'suspended' AND [dbname] = '{1}'){0}BEGIN{0}SET @status = 1;{0}END{0}SELECT @status;", Environment.NewLine, dbName)).ToList();
            var query = string.Format("IF EXISTS(SELECT * FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID(N'tempdb..#WhoStatusTemp')){0}BEGIN{0}DROP TABLE #WhoStatusTemp{0}END{0}CREATE TABLE #WhoStatusTemp(spid smallint,ecid smallint,[status] nchar(30),loginame nchar(128),hostname nchar(128),blk char(5),dbname nchar(128),cmd nchar(16),request_id int){0}INSERT #WhoStatusTemp EXEC master.sys.sp_who 'active';{0}DECLARE @status int;{0}SET @status = 0;{0}IF EXISTS(SELECT * FROM #WhoStatusTemp WHERE [dbname] = N'{1}'){0}BEGIN{0}SET @status = 1;{0}END{0}SELECT @status;", Environment.NewLine, dbName);
			var retVal = Context.Database.SqlQuery<int>(query).ToList();
            return retVal.Count > 1 ? retVal[0] == 1 : false;
        }
		
        public void Dispose()
        {
            if (Context.Database.Connection.State == ConnectionState.Open)
            {
                Context.Database.Connection.Close();
            }
            Context.Dispose();
            GC.WaitForPendingFinalizers();
        }
	}
}
