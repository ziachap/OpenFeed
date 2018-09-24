using System;
using System.Collections.Generic;
using System.Linq;
using OpenFeed.Services.Pagination;

namespace OpenFeed.Services.NewsService
{
	public class TestNewsService : INewsService
	{
		private const int PageSize = 20;
		private readonly IPaginationService _paginationService;

		public TestNewsService(IPaginationService paginationService)
		{
			_paginationService = paginationService;
		}

		public IPaginatedResults<Article> SearchArticles(NewsSearchConfiguration config)
		{
			return _paginationService.Paginate(TestArticles(), 1, PageSize);
		}

		private static IEnumerable<Article> TestArticles()
		{
			return Enumerable.Range(1, 5).Select(i => new Article
			{
				Title = "Article " + i,
				Description = "This description is about article " + i,
				Url = "#",
				ImageUrl = "https://via.placeholder.com/350x200",
				PublishDate = DateTime.Now.ToLongDateString(),
				Author = "John Smith",
				Source = "Test News",
				Category = "World"
			});
		}
	}
}