using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Common.Data.SqlServer
{
    public class SqlServerDbClient : DbClient<SqlException>
    {
        public SqlServerDbClient(Func<DbConnection> connectionFactory) : base(connectionFactory, new SqlServerRetryStrategy())
        {
        }
    }
}