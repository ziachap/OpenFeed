using Microsoft.AspNetCore.Mvc;
using OpenFeed.Services.NewsService;
using OpenFeed.Services.Pagination;
using OpenFeed.Services.RSS;

namespace OpenFeed.Controllers
{
	public class NewsApiController : Controller
	{
		private readonly INewsService _newsService;

		public NewsApiController(INewsService newsService)
		{
			_newsService = newsService;
		}

		// GET
		public IPaginatedResults<Article> Index(NewsSearchConfiguration config)
		{
			return _newsService.SearchArticles(config);
		}
	}
}