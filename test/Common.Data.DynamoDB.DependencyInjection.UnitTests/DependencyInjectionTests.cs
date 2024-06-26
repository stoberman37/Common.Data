﻿using System;
using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2.DataModel;
using Common.Data.DynamoDB;
using Common.Data.Repositories;
using Common.Data.DynamoDB.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Categories;
// ReSharper disable InconsistentNaming

namespace Common.Data.DynamoDB.DependencyInjection.UnitTests
{
	[UnitTest]
	[ExcludeFromCodeCoverage]
	public class DependencyInjectionTests
	{

		[Fact]
		public void AddDynamoDBRepository_Succeeds()
		{
			// Arrange
			// TODO: Figure out how to mock the retry strategy func 
			var services = new ServiceCollection();
			var settings = new RepositoryConfigurationOptions
			{
				DynamoDBContext = Mock.Of<IDynamoDBContext>,
				RetryStrategy = Mock.Of<IRetryStrategy>
			};

			// Act
			services.AddDynamoDBRepository<object, object>(settings);
			var provider = services.BuildServiceProvider();
			var dynamoDBContext = provider.GetService<Func<IDynamoDBContext>>();
			var dbClientFunc = provider.GetService<Func<DynamoDBClient<object, object>>>();
			var repo = provider.GetService<IRepository<DynamoDBClient<object, object>, object>>();

			// Assert
			dynamoDBContext.Should().NotBeNull();
			dbClientFunc.Should().NotBeNull();
			repo.Should().NotBeNull();
		}

		[Fact]
		public void AddDynamoDBRepository_Fails_WithNullServiceCollection()
		{
			// Arrange & Act
			var a = () =>
				(null as ServiceCollection).AddDynamoDBRepository<object, object>(new RepositoryConfigurationOptions());

			// Assert
			a.Should().NotBeNull();
			a.Should().Throw<ArgumentNullException>().WithParameterName("this");
		}

		[Fact]
		public void AddDynamoDBRepository_Fails_WithNullRepositoryConfigurationOptions()
		{
			// Arrange
			var services = new ServiceCollection();

			// Act
			var a = () => services.AddDynamoDBRepository<object, object>(null);

			// Assert
			a.Should().Throw<ArgumentNullException>()
				.WithParameterName("options");
		}

		[Fact]
		public void AddDynamoDBRepository_Fails_WithNullDynamoDBContext()
		{
			// Arrange
			var services = new ServiceCollection();
			var settings = new RepositoryConfigurationOptions() { DynamoDBContext = null };

			// Act
			var a = () => services.AddDynamoDBRepository<object, object>(settings);

			// Assert
			a.Should().Throw<ArgumentException>()
				.WithParameterName("options")
				.WithMessage("DynamoDBContext cannot be null (Parameter 'options')");
		}

		[Fact]
		public void AddDynamoDBRepository_Fails_WithNullRetryStrategy()
		{
			// Arrange
			var services = new ServiceCollection();
			var settings = new RepositoryConfigurationOptions()
			{
				DynamoDBContext = Mock.Of<IDynamoDBContext>,
				RetryStrategy = null
			};

			// Act
			var a = () => services.AddDynamoDBRepository<object, object>(settings);

			// Assert
			a.Should().Throw<ArgumentException>()
				.WithParameterName("options")
				.WithMessage("RetryStrategy cannot be null (Parameter 'options')");
		}
	}
}