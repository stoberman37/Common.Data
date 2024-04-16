using System.Data.SqlClient;

namespace Common.Data.SqlServer
{

	public class SqlServerRetryStrategy : RetryStrategyBase<SqlException>
	{
		public SqlServerRetryStrategy() 
			: base((s, ex) 
				=> s.RetryCount <= s.MaxRetryCount && (ex.Number == -2 || ex.Number == 11 || ex.Number == 1205 || ex.Number == 11001))
		{
		}
	}
}