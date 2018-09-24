using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.Filter
{
	public class TextNewsFilter : INewsFilter
	{
		public FilterDefinition<ArticleData> Filter(FilterDefinition<ArticleData> filter,
			NewsSearchConfiguration config)
		{
			if (!string.IsNullOrWhiteSpace(config.Text))
			{
				return Builder().And(filter, Builder().Text(config.Text));
			}

			return filter;
		}

		private static FilterDefinitionBuilder<ArticleData> Builder()
		{
			return Builders<ArticleData>.Filter;
		}
	}
}