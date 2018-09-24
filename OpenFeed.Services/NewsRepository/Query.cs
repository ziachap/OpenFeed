using System;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository
{
	public interface IMongoQuery<TData>
	{
		FilterDefinition<TData> Filter { get; }
		ProjectionDefinition<TData> Projection { get; }
		SortDefinition<TData> Sort { get; }
	}

	public class MongoQuery<TData> : IMongoQuery<TData>
	{
		public MongoQuery(FilterDefinition<TData> filter,
			ProjectionDefinition<TData> projection,
			SortDefinition<TData> sort)
		{
			Filter = filter;
			Projection = projection;
			Sort = sort;
		}

		public FilterDefinition<TData> Filter { get; }
		public ProjectionDefinition<TData> Projection { get; }
		public SortDefinition<TData> Sort { get; }
	}
}