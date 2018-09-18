using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPoco;

namespace OpenFeed.Services.Database
{
	public class DefaultDatabaseProvider : IDatabaseProvider
	{
		private readonly IConfiguration _configuration;

		public DefaultDatabaseProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IDatabase Database()
		{
			return new NPoco.Database(_configuration.GetConnectionString("OpenFeed"),
				DatabaseType.SqlServer2012,
				SqlClientFactory.Instance);
		}
	}
}