using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Common.Data.Specifications
{
	public interface IQuerySpecification<in TClient, T>
	{
		Func<TClient, Task<IEnumerable<T>>> ExecuteFunc();
		Func<TClient, Task<IEnumerable<T>>> ExecuteFunc(CancellationToken cancellationToken);
	}
}