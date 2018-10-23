using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPoco;

namespace OpenFeed.Services.Database
{
	public class SqlDatabaseProvider : ISqlDatabaseProvider
	{
		private readonly IConfiguration _configuration;

		public SqlDatabaseProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IDatabase Database()
		{
			return new NPoco.Database(_configuration.GetConnectionString("OpenFeedSql"),
				DatabaseType.SqlServer2012,
				SqlClientFactory.Instance);
		}
	}
}