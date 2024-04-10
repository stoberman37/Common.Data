using Common.Data.Attributes;

namespace Common.Data.Dapper.UnitTests.TestClasses
{
	public class SimplePocoWithColumnMapping
	{
		[Column("Column1")]
		public int Param1 { get; set; }
		public string Param2 { get; set; }
	}
}