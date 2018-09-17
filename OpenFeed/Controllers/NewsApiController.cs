using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenFeed.Services.NewsService;
using OpenFeed.Services.Pagination;
using OpenFeed.Services.RSS;

namespace OpenFeed.Controllers
{
    public class NewsApiController : Controller
    {
        private readonly IRssFeedService _rssFeedService;
        private readonly INewsService _newsService;

        public NewsApiController(IRssFeedService rssFeedService, INewsService newsService)
        {
            _rssFeedService = rssFeedService;
            _newsService = newsService;
        }

        // GET
        public IPaginatedResults<Article> Index(NewsSearchConfiguration config)
        {
            return _newsService.SearchArticles(config);
        }
    }
}