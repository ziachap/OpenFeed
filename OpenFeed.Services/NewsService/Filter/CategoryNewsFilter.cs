using System;
using MongoDB.Driver;
using NewsAPI.Constants;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.Filter
{
	public class CategoryNewsFilter : INewsFilter
	{
		public FilterDefinition<ArticleData> Filter(FilterDefinition<ArticleData> filter,
			NewsSearchConfiguration config)
		{
			if (config.Category.HasValue)
			{
				var builder = Builders<ArticleData>.Filter;
				return builder.And(filter, builder.Eq(data => data.Category, CategoryName(config.Category.Value)));
			}

			return filter;
		}

		private string CategoryName(Categories? category)
		{
			return category.HasValue ? Enum.GetName(typeof(Categories), category.Value) : null;
		}
	}
}