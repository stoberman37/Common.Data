using System;
using Common.Data.Repositories;
using D = Dapper;

// ReSharper disable once CheckNamespace
namespace Common.Data.Dapper
{
	public static class DependencyInjection
	{
		public static IRepository<TClient, TReturn> AddColumnMapper<TClient, TReturn>(this IRepository<TClient, TReturn> @this, bool caseSensitive = false)
			where TClient : class, IDisposable
			where TReturn : class

		{
			D.SqlMapper.SetTypeMap(
				typeof(TReturn),
				new ColumnAttributeTypeMapper<TReturn>(caseSensitive));
			return @this;
		}
	}
}
