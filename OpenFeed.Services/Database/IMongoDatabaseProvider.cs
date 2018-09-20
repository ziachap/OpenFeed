using MongoDB.Driver;

namespace OpenFeed.Services.Database
{
    public interface IMongoDatabaseProvider
	{
	    IMongoDatabase Database();
    }
}
