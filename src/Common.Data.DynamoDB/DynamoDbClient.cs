﻿using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Data.DynamoDB
{
	// ReSharper disable once InconsistentNaming
	public class DynamoDBClient<T, TKey> : IDisposable
		where T : class
		where TKey : notnull
	{
		private readonly IDynamoDBContext _dynamoDbContext;
		private readonly IRetryStrategy _retryStrategy;
		private bool _disposedValue;

		public DynamoDBClient(Func<IDynamoDBContext> connectionFactory) : this(connectionFactory, new RetryStrategyByCount())
		{
		}

		public DynamoDBClient(Func<IDynamoDBContext> connectionFactory, IRetryStrategy retryStrategy)
		{
			if (connectionFactory == null) throw new ArgumentNullException(nameof(connectionFactory));
			_retryStrategy = retryStrategy ?? throw new ArgumentNullException(nameof(retryStrategy));
			_dynamoDbContext = connectionFactory();
		}

		public Task CreateAsync(T toSave)
		{
			if (toSave == default(T))  throw new ArgumentNullException(nameof(toSave));
			return _retryStrategy.RetryAsync(() => _dynamoDbContext.SaveAsync(toSave));
		}

		public Task UpdateAsync(T toSave)
		{
			if (toSave == default(T)) throw new ArgumentNullException(nameof(toSave));
			return _retryStrategy.RetryAsync(() => _dynamoDbContext.SaveAsync(toSave));
		}

		public Task DeleteAsync(T toDelete)
		{
			if (toDelete == default(T)) throw new ArgumentNullException(nameof(toDelete));
			return _retryStrategy.RetryAsync(() => _dynamoDbContext.DeleteAsync(toDelete));
		}

		public Task<T> ReadAsync(TKey toRead)
		{
			if (toRead == null || toRead.Equals(default(TKey))) throw new ArgumentNullException(nameof(toRead));
			return _dynamoDbContext.LoadAsync<T>(toRead);
		}

		public Task BatchWriteAsync(IEnumerable<T> toSave)
		{
			var batch = _dynamoDbContext.CreateBatchWrite<T>();
			batch.AddPutItems(toSave);
			return _retryStrategy.RetryAsync(() => batch.ExecuteAsync());
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}
				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}