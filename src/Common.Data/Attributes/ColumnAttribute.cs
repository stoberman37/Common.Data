using System;

namespace Common.Data.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ColumnAttribute : Attribute
	{
		public ColumnAttribute()
		{
		}

		public ColumnAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; }
	}
}