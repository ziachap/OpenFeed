using System;
using System.Collections.Generic;
using System.Linq;
using NewsAPI.Constants;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly IArticleRepository _articleRepository;

        public NewsService(IArticleRepository articleRepository)
        {
	        _articleRepository = articleRepository;
        }

        public IEnumerable<Article> SearchArticles(NewsSearchConfiguration config)
        {
            return ArticlesFromApi().Where(a => IsInCategory(a, config.Category));
        }

        private IEnumerable<Article> ArticlesFromApi()
        {
	        return _articleRepository.GetAll()
		        .Select(ToArticle)
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