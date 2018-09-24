using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.QueryBuilder
{
	public interface INewsFilter
	{
		FilterDefinition<ArticleData> Filter(FilterDefinition<ArticleData> filter, NewsSearchConfiguration config);
	}
}