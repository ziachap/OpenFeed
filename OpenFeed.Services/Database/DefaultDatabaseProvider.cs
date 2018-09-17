using System.Data.SqlClient;
using NPoco;

namespace OpenFeed.Services.Database
{
	public class DefaultDatabaseProvider : IDatabaseProvider
	{
		public IDatabase Database()
		{
			return new NPoco.Database("Server=localhost;Database=OpenFeed;Trusted_Connection=True;",
				DatabaseType.SqlServer2012,
				SqlClientFactory.Instance);
		}
	}
}