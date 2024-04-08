using System;

namespace Common.Data
{
	public class RetryStrategyByCount : RetryStrategyBase<Exception>
	{
		public RetryStrategyByCount() : base((s, ex) => s.RetryCount <= s.MaxRetryCount)
		{
		}
	}
}