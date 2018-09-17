using NewsAPI.Constants;

namespace OpenFeed.Services.NewsService
{
    public class NewsSearchConfiguration
    {
		public Categories? Category => (Categories?)CategoryId;

	    public int? CategoryId { get; set; }
    }
}