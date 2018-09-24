using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.Filter
{
	public interface INewsFilter
	{
		FilterDefinition<ArticleData> Filter(FilterDefinition<ArticleData> filter, NewsSearchConfiguration config);
	}
}