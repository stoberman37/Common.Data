using System;
using System.Diagnostics.CodeAnalysis;
using Common.Data.Repositories;
using Elastic.Clients.Elasticsearch;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Categories;

namespace Common.Data.Elasticsearch.DependencyInjection.UnitTests
{
	[UnitTest]
	[ExcludeFromCodeCoverage]
	public class DependencyInjectionTests
	{

		[Fact]
		public void AddElasticsearchRepository_Succeeds()
		{
			// Arrange
			// TODO: Figure out how to mock the retry strategy func 
			var services = new ServiceCollection();
			var settings = new RepositoryConfigurationOptions
				{ ElasticsearchClientSettings = new ElasticsearchClientSettings(), RetryStrategy = Mock.Of<IRetryStrategy> };

			// Act
			services.AddElasticsearchRepository<object>(settings);
			var provider = services.BuildServiceProvider();
			var repo = provider.GetService<IRepository<CommonElasticsearchClient, object>>();

			// Assert
			repo.Should().NotBeNull();
		}

		[Fact]
		public void AddElasticsearchRepository_Fails_WithNullServiceCollection()
		{
			// Arrange & Act
			var a = () =>
				(null as ServiceCollection).AddElasticsearchRepository<object>(new RepositoryConfigurationOptions());

			// Assert
			a.Should().NotBeNull();
			a.Should().Throw<ArgumentNullException>().WithParameterName("this");
		}

		[Fact]
		public void AddElasticsearchRepository_Fails_WithNullElasticsearchClientSettings()
		{
			// Arrange
			var services = new ServiceCollection();
			var settings = new RepositoryConfigurationOptions() { ElasticsearchClientSettings = null };

			// Act
			var a = () => services.AddElasticsearchRepository<object>(settings);

			// Assert
			a.Should().Throw<ArgumentException>()
				.WithParameterName("options")
				.WithMessage("ElasticsearchClientSettings cannot be null (Parameter 'options')");
		}

		[Fact]
		public void AddElasticsearchRepository_Fails_WithNullRetryStrategy()
		{
			// Arrange
			var services = new ServiceCollection();
			var settings = new RepositoryConfigurationOptions()
			{
				ElasticsearchClientSettings = new ElasticsearchClientSettings(),
				RetryStrategy = null
			};

			// Act
			var a = () => services.AddElasticsearchRepository<object>(settings);

			// Assert
			a.Should().Throw<ArgumentException>()
				.WithParameterName("options")
				.WithMessage("RetryStrategy cannot be null (Parameter 'options')");
		}
	}
}