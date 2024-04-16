using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Common.Data;
using Common.Data.Attributes;
using Common.Data.Dapper;
using Common.Data.Example;
using Common.Data.Repositories;
using Common.Data.Specifications;
using Common.Data.SqlServer;


Console.WriteLine("Hello World!");


//serviceCollection.AddColumnMapper(typeof(DbModel), true);
//Dapper.SqlMapper.SetTypeMap(
//	typeof(DbModel),
//	new ColumnAttributeTypeMapper<DbModel>());

var connectionString = @"Server=(localdb)\mydb;Database=master;Trusted_Connection=True;";
SqlConnection ConnectionFactory() => new(connectionString);
var clientRepo = new Repository<IDbClient, DbModel>(() => new SqlServerDbClient(ConnectionFactory));
clientRepo.AddColumnMapper(true);

var clientSpec = new GetDatabaseSpec("master");
var model = clientRepo.ExecuteDbAction(clientSpec).FirstOrDefault();
Console.WriteLine($"Id: {model.Id}, Name: {model.DbName}");

namespace Common.Data.Example
{
	public class DbModel
	{
		[Column("name")]
		public string DbName { get; set; }
		[Column("database_id")]
		public int Id { get; set; }
	}

	public class GetDatabaseSpec(string dbName) : IQuerySpecification<IDbClient, DbModel>
	{
		private readonly string _dbName = dbName ?? throw new ArgumentNullException(nameof(dbName));

		public Func<IDbClient, IEnumerable<DbModel>> Execute() => Execute(new CancellationToken());

		public Func<IDbClient, IEnumerable<DbModel>> Execute(CancellationToken cancellationToken)
		{
			return db =>
				db.SetCommandText("SELECT database_id, Name from sys.databases WHERE name = @name")
					.SetCommandType(CommandType.Text)
					.AddNamedParameters(new { name = _dbName })
					.SetCommandTimeout(30)
					.ExecuteQuery<DbModel>(cancellationToken: cancellationToken);
		}
	}
	/*
	attribute-values "{\":uuid\":{\"S\":\"{ \\\"vin\\\": \\\"ABCDEF0123456GHIJK\\\" }\"},
	\":index\":{\"S\":\"{ \\\"ipaddress\\\": \\\"1.1.1.2\\\", \\\"created_date\\\": \\\"1639000000\\\" }\"}}"
	--expression-attribute-names "{\"#uuid\": \"uuid\", \"#index\": \"index\"}" --limit 1
	*/
	//	public class JerrysQuerySpec : IQuerySpecification<IDynamoDbClient, JerryModel>
	//	{
	//		private readonly string _uuid;
	//		private readonly string _index;
	//		public JerrysQuerySpec(string uuid, string index)
	//		{
	//			_uuid = uuid;
	//			_index = index;
	//		}

	//		public Func<IDynamoDbClient, Task<JerryModel>> ExecuteFunc()
	//		{
	//			var command = $"attribute-values "{\":uuid\":{\"S\":\"{ \\\"vin\\\": \\\"ABCDEF0123456GHIJK\\\" }\"},
	//\":index\":{\"S\":\"{ \\\"ipaddress\\\": \\\"1.1.1.2\\\", \\\"created_date\\\": \\\"1639000000\\\" }\"}}" 
	//--expression-attribute-names "{\"#uuid\": \"{_uuid}\", \"#index\": \"index\"}" --limit 1"
	//"
	//			return db => db.ExecuteLoadAsync(command);
	//		}
	//	}
}