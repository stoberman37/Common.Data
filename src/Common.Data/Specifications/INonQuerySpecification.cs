using System;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Common.Data.Specifications
{
	public interface INonQuerySpecification<in TClient>
	{
		Func<TClient, Task> ExecuteFunc();
		Func<TClient, Task> ExecuteFunc(CancellationToken cancellationToken);
	}
}
