using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository.Sort
{
	public class RelevancySort<T> : ISort<T> where T : ITextSearchSortable
	{
		public SortDefinition<T> AsMongoSortDefintion() => Builders<T>.Sort.MetaTextScore("TextMatchScore");
	}
}