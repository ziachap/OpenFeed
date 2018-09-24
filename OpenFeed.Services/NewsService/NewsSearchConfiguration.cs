using NewsAPI.Constants;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService
{
    public class NewsSearchConfiguration
    {
		public Categories? Category => (Categories?)CategoryId;

	    public int? CategoryId { get; set; }

		public SortType SortType => (SortType)SortTypeId;

	    public int SortTypeId { get; set; }

		public int Page { get; set; }

		public string Text { get; set; }
    }
}