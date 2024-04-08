using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Common.Data.Repositories;
using Common.Data.Specifications;
using FluentAssertions;
using Xunit.Categories;

#pragma warning disable IDE0062
namespace Common.Data.UnitTests
{
	[UnitTest]
	public class RepositoryTests
	{
		[Fact]
		public void ConstructorTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;

			// Act
			var repo = new Repository<IDbClient, object>(Factory);

			// Assert
			repo.Should().NotBeNull();
		}

		[Fact]
		public void ConstructorWithNullFactoryTest()
		{
			// Arrange & Act
			var a = () => new Repository<IDbClient, object>(null);

			// Assert
			a.Should()
				.Throw<ArgumentNullException>()
				.WithParameterName("factory");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncActionNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;

			// Act
			var repo = new Repository<IDbClient, object>(Factory);
			var a = () => repo.ExecuteDbActionAsync(null as Func<IDbClient, Task<IEnumerable<object>>>);

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("action");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;

			// Act
			var repo = new Repository<IDbClient, object>(Factory);
			var a = ()=> repo.ExecuteDbActionAsync((Func<IDbClient, Task>)null);

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("action");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			Task Task(IDbClient c) => System.Threading.Tasks.Task.FromResult(true);

			// Act
			var repo = new Repository<IDbClient, object>(Factory);
			var a = () => repo.ExecuteDbActionAsync(Task);
			

			// Assert
			await a.Should()
				.NotThrowAsync<Exception>();
		}

		[Fact]
		public async Task ExecuteDbActionAsyncActionWithReturnNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbActionAsync(null as Func<IDbClient, Task<IEnumerable<object>>>);

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("action");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithReturnTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			Task<IEnumerable<object>> DbAction(IDbClient c) =>
				Task.FromResult(new List<object>() as IEnumerable<object>);
			var repo = new Repository<IDbClient, object>(Factory);


			// Act
			var result = await repo.ExecuteDbActionAsync(DbAction);

			// Assert
			result.Should().NotBeNull();
		}


		[Fact]
		public void ExecuteDbActionActionNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbAction((Action<IDbClient>) null);

			// Assert
			a.Should()
				.Throw<ArgumentNullException>()
				.WithParameterName("action");
		}

		[Fact]
		public void ExecuteDbActionTest()
		{
			// Arrange
			var repo = new Repository<IDbClient, object>(Factory);
			IDbClient Factory() => new Mock<IDbClient>().Object;
			void Func(IDbClient c)
			{
			}

			// Act
			var a = () => repo.ExecuteDbAction((Action<IDbClient>) Func);

			// Assert
			a.Should().NotThrow<Exception>();
		}

		[Fact]
		public void ExecuteDbActionActionWithReturnNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbAction(null);

			// Assert
			a.Should()
				.Throw<ArgumentNullException>()
				.WithParameterName("action");
		}

		[Fact]
		public void ExecuteDbActionWithReturnTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			IEnumerable<object> DbAction(IDbClient c) => new List<object>();
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var result = repo.ExecuteDbAction(DbAction);

			// Assert
			result.Should().NotBeNull();
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithNonQuerySpecificationNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbActionAsync(null as INonQuerySpecification<IDbClient>);

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("specification");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithNonQuerySpecificationAndCancellationNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbActionAsync(null as INonQuerySpecification<IDbClient>, new CancellationToken());

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("specification");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithNonQuerySpecificationTest()
		{
			// Arrange
			var spec = new Mock<INonQuerySpecification<IDbClient>>();
			Task Task(IDbClient c) => System.Threading.Tasks.Task.FromResult(true);
			spec.Setup(s => s.ExecuteFunc()).Returns(Task);
			spec.Setup(s => s.ExecuteFunc(It.IsAny<CancellationToken>())).Returns(Task);
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			await repo.ExecuteDbActionAsync(spec.Object);

			// Assert
			spec.Verify(s => s.ExecuteFunc(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithNonQuerySpecificationAndCancellationTokenTest()
		{
			// Arrange
			var spec = new Mock<INonQuerySpecification<IDbClient>>();
			Task Task(IDbClient c) => System.Threading.Tasks.Task.FromResult(true);
			spec.Setup(s => s.ExecuteFunc()).Returns(Task);
			spec.Setup(s => s.ExecuteFunc(It.IsAny<CancellationToken>())).Returns(Task);
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);
			var cancelToken = new CancellationToken();

			// Act
			await repo.ExecuteDbActionAsync(spec.Object, cancelToken);

			// Assert
			spec.Verify(s => s.ExecuteFunc(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithQuerySpecificationNullTest()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbActionAsync(null as IQuerySpecification<IDbClient, object>);

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("specification");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithQuerySpecificationAndCancellationNullTests()
		{
			// Arrange
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			var a = () => repo.ExecuteDbActionAsync(null as IQuerySpecification<IDbClient, object>, new CancellationToken());

			// Assert
			await a.Should()
				.ThrowAsync<ArgumentNullException>()
				.WithParameterName("specification");
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithQuerySpecificationTest()
		{
			// Arrange
			var spec = new Mock<IQuerySpecification<IDbClient, object>>();

			Task<IEnumerable<object>> DbAction(IDbClient c) =>
				Task.FromResult(new List<object>() as IEnumerable<object>);
			spec.Setup(s => s.ExecuteFunc()).Returns(DbAction);
			spec.Setup(s => s.ExecuteFunc(It.IsAny<CancellationToken>())).Returns(DbAction);
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);

			// Act
			await repo.ExecuteDbActionAsync(spec.Object);

			// Assert
			spec.Verify(s => s.ExecuteFunc(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task ExecuteDbActionAsyncWithQuerySpecificationAndCancellationTokenTest()
		{
			// Arrange
			var spec = new Mock<INonQuerySpecification<IDbClient>>();
			Task<IEnumerable<object>> DbAction(IDbClient c) =>
				Task.FromResult(new List<object>() as IEnumerable<object>);
			spec.Setup(s => s.ExecuteFunc()).Returns(DbAction);
			spec.Setup(s => s.ExecuteFunc(It.IsAny<CancellationToken>())).Returns(DbAction);
			IDbClient Factory() => new Mock<IDbClient>().Object;
			var repo = new Repository<IDbClient, object>(Factory);
			var cancelToken = new CancellationToken();

			// Act
			await repo.ExecuteDbActionAsync(spec.Object, cancelToken);

			// Assert
			spec.Verify(s => s.ExecuteFunc(It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
