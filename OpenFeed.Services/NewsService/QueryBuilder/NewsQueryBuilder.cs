using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.QueryBuilder
{
	public class NewsQueryBuilder : INewsQueryBuilder
	{
		private readonly IEnumerable<INewsFilter> _filters;

		public NewsQueryBuilder(IEnumerable<INewsFilter> filters)
		{
			_filters = filters;
		}

		public FilterDefinition<ArticleData> BuildQuery(NewsSearchConfiguration config)
		{
			return _filters.Aggregate(FilterDefinition<ArticleData>.Empty, (current, filter) => filter.Filter(current, config));
		}
	}
}