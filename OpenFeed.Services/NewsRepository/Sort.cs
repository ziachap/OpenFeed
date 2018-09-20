using System;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository
{
	public interface ISort<T>
	{
		SortDefinition<T> AsMongoSortDefintion();
	}

	public class Sort<T> : ISort<T>
	{
		private readonly Expression<Func<T, object>> _fieldSelector;
		private readonly SortDirection _direction;

		public Sort(Expression<Func<T, object>> fieldSelector, SortDirection direction)
		{
			_fieldSelector = fieldSelector;
			_direction = direction;
		}
		
		public SortDefinition<T> AsMongoSortDefintion()
		{
			var fieldName = GetMemberName(_fieldSelector);

			switch (_direction)
			{
				case SortDirection.Ascending:
					return Builders<T>.Sort.Ascending(fieldName);
				case SortDirection.Descending:
					return Builders<T>.Sort.Descending(fieldName);
				default:
					throw new Exception("SortDirection not supported: " + _direction);
			}
		}

		private string GetMemberName<T>(Expression<T> expression)
		{
			switch (expression.Body)
			{
				case MemberExpression m:
					return m.Member.Name;
				case UnaryExpression u when u.Operand is MemberExpression m:
					return m.Member.Name;
				default:
					throw new NotImplementedException(expression.GetType().ToString());
			}
		}
	}
	
	public enum SortDirection
	{
		Ascending,
		Descending
	}
}