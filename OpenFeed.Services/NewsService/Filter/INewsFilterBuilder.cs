using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.Filter
{
	public interface INewsFilterBuilder
	{
		FilterDefinition<ArticleData> BuildQuery(NewsSearchConfiguration config);
	}
}