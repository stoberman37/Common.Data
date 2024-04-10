﻿using System;
using System.Data;
using Xunit;

namespace Common.Data.Dapper.UnitTests
{
	public class OdbcParameterTestData : TheoryData<string, DbType, Type, object, ParameterDirection, object>
	{
		public OdbcParameterTestData()
		{
			var now = DateTime.Now;
			Add("AnsiStringParam", DbType.AnsiString, typeof(string), "ansiString", ParameterDirection.InputOutput,
				"ansiString");
			Add("ByteParam", DbType.Byte, typeof(byte), (byte)64, ParameterDirection.InputOutput, (byte)64);
			Add("BooleanParam", DbType.Boolean, typeof(bool), true, ParameterDirection.InputOutput, true);
			Add("CurrencyParam", DbType.Currency, typeof(decimal), (decimal)123.45, ParameterDirection.InputOutput, (decimal)123.45);
			Add("DateParam", DbType.Date, typeof(DateTime), now.Date, ParameterDirection.InputOutput, now.Date);
			Add("DateTimeParam", DbType.DateTime, typeof(DateTime), now, ParameterDirection.InputOutput, now);
			Add("DecimalParam", DbType.Decimal, typeof(decimal), (decimal)123.45, ParameterDirection.InputOutput,
				(decimal)123.45);
			Add("DoubleParam", DbType.Double, typeof(double), 6789.01234, ParameterDirection.InputOutput, 6789.01234);
			Add("GuidParam", DbType.Guid, typeof(Guid), new Guid("12345678-9ABC-DEF0-1234-56789ABCDEF0"),
				ParameterDirection.InputOutput, new Guid("12345678-9ABC-DEF0-1234-56789ABCDEF0"));
			Add("Int16Param", DbType.Int16, typeof(short), (short)16, ParameterDirection.InputOutput, (short)16);
			Add("Int32Param", DbType.Int32, typeof(int), 32, ParameterDirection.InputOutput, 32);
			Add("Int64Param", DbType.Int64, typeof(long), (long)64, ParameterDirection.InputOutput, (long)64);
			Add("SingleParam", DbType.Single, typeof(float), (float)123.45, ParameterDirection.InputOutput,
				(float)123.45);
			Add("StringParam", DbType.String, typeof(string), "string", ParameterDirection.InputOutput, "string");
			Add("AnsiStringFixedLengthParam", DbType.AnsiStringFixedLength, typeof(string),
				"AnsiStringFixedLengthParam", ParameterDirection.InputOutput, "AnsiStringFixedLengthParam");
			Add("StringFixedLengthParam", DbType.StringFixedLength, typeof(string), "StringFixedLengthParam",
				ParameterDirection.InputOutput, "StringFixedLengthParam");

			Add("AnsiStringParam", DbType.AnsiString, typeof(string), "ansiString", ParameterDirection.Output,
				"ansiString");
			Add("ByteParam", DbType.Byte, typeof(byte), (byte)64, ParameterDirection.Output, (byte)64);
			Add("BooleanParam", DbType.Boolean, typeof(bool), true, ParameterDirection.Output, true);
			Add("CurrencyParam", DbType.Currency, typeof(decimal), (decimal)123.45, ParameterDirection.Output, (decimal)123.45);
			Add("DateParam", DbType.Date, typeof(DateTime), now.Date, ParameterDirection.Output, now.Date);
			Add("DateTimeParam", DbType.DateTime, typeof(DateTime), now, ParameterDirection.Output, now);
			Add("DecimalParam", DbType.Decimal, typeof(decimal), (decimal)123.45, ParameterDirection.Output,
				(decimal)123.45);
			Add("DoubleParam", DbType.Double, typeof(double), 6789.01234, ParameterDirection.Output, 6789.01234);
			Add("GuidParam", DbType.Guid, typeof(Guid), new Guid("12345678-9ABC-DEF0-1234-56789ABCDEF0"),
				ParameterDirection.Output, new Guid("12345678-9ABC-DEF0-1234-56789ABCDEF0"));
			Add("Int16Param", DbType.Int16, typeof(short), (short)16, ParameterDirection.Output, (short)16);
			Add("Int32Param", DbType.Int32, typeof(int), 32, ParameterDirection.Output, 32);
			Add("Int64Param", DbType.Int64, typeof(long), (long)64, ParameterDirection.Output, (long)64);
			Add("SingleParam", DbType.Single, typeof(float), (float)123.45, ParameterDirection.Output, (float)123.45);
			Add("StringParam", DbType.String, typeof(string), "string", ParameterDirection.Output, "string");
			Add("AnsiStringFixedLengthParam", DbType.AnsiStringFixedLength, typeof(string),
				"AnsiStringFixedLengthParam", ParameterDirection.Output, "AnsiStringFixedLengthParam");
			Add("StringFixedLengthParam", DbType.StringFixedLength, typeof(string), "StringFixedLengthParam",
				ParameterDirection.Output, "StringFixedLengthParam");
		}
	}

	public class OdbcParameterExtractFailureTestData : TheoryData<string, DbType, ParameterDirection, object, Type>
	{
		public OdbcParameterExtractFailureTestData()
		{
			Add("BinaryParam", DbType.Binary, ParameterDirection.InputOutput, null, typeof(NotImplementedException));
			Add("TimeParam", DbType.Time, ParameterDirection.InputOutput, null, typeof(NotImplementedException));

			Add("BinaryParam", DbType.Binary, ParameterDirection.Output, null, typeof(NotImplementedException));
			Add("TimeParam", DbType.Time, ParameterDirection.Output, null, typeof(NotImplementedException));

			// The following DbType types are not supported by SqlDbType
			//SByteParam
			//ObjectParam
			//DateTime2Param
			//XmlParam
			//DateTimeOffsetParam
			//UInt16Param
			//UInt32Param
			//UInt64Param
			//VarNumericParam
		}
	}
}
