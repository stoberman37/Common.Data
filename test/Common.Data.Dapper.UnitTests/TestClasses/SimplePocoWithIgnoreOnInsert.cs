using Common.Data.Attributes;

namespace Common.Data.Dapper.UnitTests.TestClasses
{
	public class SimplePocoWithIgnoreOnInsert
	{
		[IgnoreOnInsert]
		public int Param1 { get; set; }
		public string Param2 { get; set; }
	}
}