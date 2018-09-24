namespace OpenFeed.Services.NewsRepository.Sort
{
	public interface ITextSearchSortable
	{
		double? TextMatchScore { get; set; }
	}
}