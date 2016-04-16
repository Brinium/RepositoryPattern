using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    [System.Serializable]
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException() { }
        public ConcurrencyException(string message) : base(message) { }
        public ConcurrencyException(string message, System.Exception inner) : base(message, inner) { }
        protected ConcurrencyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
	
    [System.Serializable]
    public class DataException : System.Exception
    {
        public DataException() { }
        public DataException(string message) : base(message) { }
        public DataException(string message, System.Exception inner) : base(message, inner) { }
        protected DataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
	
    [System.Serializable]
    public class RepositoryAccessException : System.Exception
    {
        public RepositoryAccessException() { }
        public RepositoryAccessException(string message) : base(message) { }
        public RepositoryAccessException(string message, System.Exception inner) : base(message, inner) { }
        protected RepositoryAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
