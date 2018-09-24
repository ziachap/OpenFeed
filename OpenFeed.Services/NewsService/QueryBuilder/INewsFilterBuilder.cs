using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.QueryBuilder
{
    public interface INewsFilterBuilder
    {
	    FilterDefinition<ArticleData> BuildQuery(NewsSearchConfiguration config);
    }
}
