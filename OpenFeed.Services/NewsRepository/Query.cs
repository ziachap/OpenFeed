using System;
using System.Linq.Expressions;

namespace OpenFeed.Services.NewsRepository
{
	// TODO: Make some local query type with a builder type
	// Depreciated
	public interface IQuery<TData>
	{
		Expression<Func<TData, bool>> AsExpression();
	}

	public class Query<TData> : IQuery<TData>
	{
		private readonly Func<TData, bool> _criteria;

		public Query(Func<TData, bool> criteria)
		{
			_criteria = criteria;
		}

		public Expression<Func<TData, bool>> AsExpression() => d => _criteria(d);
	}
}