using System;
using System.Data.Common;
using Xunit;
using Xunit.Categories;
using FluentAssertions;
using Moq;

namespace Common.Data.SqlServer.UnitTests
{
	[UnitTest]
	public class SqlServerDbClientTests
	{
		[Fact]
		public void Constructor_Succeeds()
		{
			// Arrange
			DbConnection Factory() => Mock.Of<DbConnection>();

			// Act
			var client = new SqlServerDbClient(Factory);

			// Assert
			client.Should().NotBeNull();
		}
	}

	[UnitTest]
	public class SqlServerRetryStrategyTests
	{
		[Fact]
		public void Constructor_Succeeds()
		{
			// Arrange
			//var str = () => Mock.Of<DbConnection>();

			// Act
			var strategy = new SqlServerRetryStrategy();

			// Assert
			strategy.Should().NotBeNull();
		}
	}
}
