using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository.Sort
{
	public interface ISort<T>
	{
		SortDefinition<T> AsMongoSortDefintion();
	}
}