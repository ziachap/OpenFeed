using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using NewsAPI.Constants;
using OpenFeed.Services.NewsRepository;
using OpenFeed.Services.Pagination;
using SortDirection = OpenFeed.Services.NewsRepository.SortDirection;

namespace OpenFeed.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly IQueryableArticleRepository _articleRepository;
        private readonly IPaginationService _paginationService;
        private readonly ISortFactory<ArticleData> _sortFactory;
		private const int PageSize = 20;

        public NewsService(IQueryableArticleRepository articleRepository, IPaginationService paginationService, ISortFactory<ArticleData> sortFactory)
        {
	        _articleRepository = articleRepository;
	        _paginationService = paginationService;
	        _sortFactory = sortFactory;
        }

        public IPaginatedResults<Article> SearchArticles(NewsSearchConfiguration config)
	    {
		    return _paginationService.Paginate(ArticlesFromApi(config), config.Page, PageSize);
	    }

	    private IEnumerable<Article> ArticlesFromApi(NewsSearchConfiguration config)
	    {
			return _articleRepository.GetMany(Filter(config), _sortFactory.Make(config.SortType))
		        .Select(ToArticle)
		        .ToList();
        }

	    FilterDefinition<ArticleData> Filter(NewsSearchConfiguration config)
	    {
		    return config.Category.HasValue 
			    ? Builders<ArticleData>.Filter.Eq(data => data.Category, CategoryName(config.Category.Value))
			    : FilterDefinition<ArticleData>.Empty;
	    }

		private string CategoryName(Categories? category) 
			=> category.HasValue ? Enum.GetName(typeof(Categories), category.Value) : null;

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