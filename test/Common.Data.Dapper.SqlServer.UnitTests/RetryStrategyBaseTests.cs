
using System;
using System.Reflection;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Categories;

namespace Common.Data.UnitTests
{

#pragma warning disable IDE0062
	[UnitTest]
	public class RetryStrategyBaseTests
	{
		[Fact]
		public void ConstructorTests()
		{
			// Arrange
			bool ContinueFunc(RetryStrategyBase<Exception> r, Exception e) => false;

			// Act
			var success =
				new Mock<RetryStrategyBase<Exception>>(
					(Func<RetryStrategyBase<Exception>, Exception, bool>) ContinueFunc);
			var a = () => new Mock<RetryStrategyBase<Exception>>(null).Object;

			// Assert
			a.Should()
				.Throw<TargetInvocationException>()
				.WithInnerException<ArgumentNullException>()
				.WithParameterName("continueFunc");
		}
	}
}
