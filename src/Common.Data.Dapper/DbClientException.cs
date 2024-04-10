using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

// ReSharper disable once CheckNamespace
namespace Common.Data
{
    [Serializable]
    public class DbClientException : Exception
    {
        public DbClientException()
        {
        }

        public DbClientException(string message)
            : base(message)
        {
        }

        public DbClientException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}