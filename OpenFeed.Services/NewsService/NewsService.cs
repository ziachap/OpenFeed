using System;
using System.Collections.Generic;
using System.Linq;
using NewsAPI.Constants;
using OpenFeed.Services.NewsRepository;
using OpenFeed.Services.Pagination;

namespace OpenFeed.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IPaginationService _paginationService;
		private const int PageSize = 20;

        public NewsService(IArticleRepository articleRepository, IPaginationService paginationService)
        {
	        _articleRepository = articleRepository;
	        _paginationService = paginationService;
        }

        public IPaginatedResults<Article> SearchArticles(NewsSearchConfiguration config)
	    {
		    return _paginationService.Paginate(ArticlesFromApi(config), config.Page, PageSize);
	    }

	    private IEnumerable<Article> ArticlesFromApi(NewsSearchConfiguration config)
        {
	        return _articleRepository.GetAll()
		        .Select(ToArticle)
		        .Where(a => IsInCategory(a, config.Category))
				.OrderByDescending(x => x.PublishDate)
		        .ToList();
        }

	    private bool IsInCategory(Article article, Categories? category) 
		    => !category.HasValue || Enum.GetName(typeof(Categories), category.Value) == article.Category;

	    // This is to be removed eventually
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

        private static Article ToArticle(ArticleData article)
        {
            return new Article
            {
                Title = article.Title,
                Description = article.Description,
                Url = article.Url,
                ImageUrl = article.ImageUrl,
                PublishDate = article.PublishDate?.ToLongDateString() ?? string.Empty,
                Author = article.Author,
                Source = article.Source,
				Category = article.Category
            };
        }
    }
}