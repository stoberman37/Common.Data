using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Data.Specifications;

// ReSharper disable once CheckNamespace
namespace Common.Data.Repositories
{
    public interface IRepository<out TClient, TReturn>
        where TClient : class, IDisposable
        where TReturn : class
    {
        // Synchronous calls
        void ExecuteDbAction(Action<TClient> action);
        IEnumerable<TReturn> ExecuteDbAction(Func<TClient, IEnumerable<TReturn>> action);

        // Async calls
        Task ExecuteDbActionAsync(Func<TClient, Task> action);
        Task<IEnumerable<TReturn>> ExecuteDbActionAsync(Func<TClient, Task<IEnumerable<TReturn>>> action);
        Task ExecuteDbActionAsync(INonQuerySpecification<TClient> specification);
        Task ExecuteDbActionAsync(INonQuerySpecification<TClient> specification, CancellationToken cancellationToken);
        Task<IEnumerable<TReturn>> ExecuteDbActionAsync(IQuerySpecification<TClient, TReturn> specification);
        Task<IEnumerable<TReturn>> ExecuteDbActionAsync(IQuerySpecification<TClient, TReturn> specification, CancellationToken cancellationToken);
    }
}