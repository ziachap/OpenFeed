﻿using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService.Filter
{
	public class NewsFilterBuilder : INewsFilterBuilder
	{
		private readonly IEnumerable<INewsFilter> _filters;

		public NewsFilterBuilder(IEnumerable<INewsFilter> filters)
		{
			_filters = filters;
		}

		public FilterDefinition<ArticleData> BuildQuery(NewsSearchConfiguration config)
		{
			return _filters.Aggregate(FilterDefinition<ArticleData>.Empty,
				(current, filter) => filter.Filter(current, config));
		}
	}
}