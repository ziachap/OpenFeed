using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace OpenFeed.Services.Database
{
	public class MongoDatabaseProvider : IMongoDatabaseProvider
	{
		private readonly IConfiguration _configuration;

		public MongoDatabaseProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IMongoDatabase Database()
		{
			var mongoClient = new MongoClient(_configuration.GetConnectionString("OpenFeedMongo"));
			return mongoClient.GetDatabase("OpenFeed");
		}
	}
}