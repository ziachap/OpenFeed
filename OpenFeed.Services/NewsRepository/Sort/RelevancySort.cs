using System;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository.Sort
{
	public interface ITextSearchSortable
	{
		double? TextMatchScore { get; set; }
	}

	public class RelevancySort<T> : ISort<T> where T : ITextSearchSortable
	{
		public SortDefinition<T> AsMongoSortDefintion() => Builders<T>.Sort.MetaTextScore("TextMatchScore");
	}
}