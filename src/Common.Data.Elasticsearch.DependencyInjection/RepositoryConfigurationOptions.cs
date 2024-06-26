﻿using System;
using Elastic.Clients.Elasticsearch;

namespace Common.Data.Elasticsearch.DependencyInjection
{

	public class RepositoryConfigurationOptions
	{
		public ElasticsearchClientSettings ElasticsearchClientSettings { get; set; }
		public Func<IRetryStrategy> RetryStrategy { get; set; } = () => new RetryStrategyByCount(0);
	}
}