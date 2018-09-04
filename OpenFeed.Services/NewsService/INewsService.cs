using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using NewsAPI.Constants;
using NewsAPI.Models;
using OpenFeed.Services.NewsAPI;
using NewsApiArticle = NewsAPI.Models.Article;

namespace OpenFeed.Services.NewsService
{
    public interface INewsService
    {
        IEnumerable<Article> SearchArticles();
    }

    public class NewsService : INewsService
    {
        private readonly INewsApiClientProvider _clientProvider;
        private readonly IMemoryCache _cache;

        public NewsService(INewsApiClientProvider clientProvider, IMemoryCache cache)
        {
            _clientProvider = clientProvider;
            _cache = cache;
        }

        public IEnumerable<Article> SearchArticles()
        {
            //return TestArticles();

            // TODO: Use a call layer and do not call directly from client provider
            // TODO: Use your own config type and map to TopHeadlinesRequest in call layer

            return _cache.GetOrCreate("_Articles", entry => ArticlesFromApi());

            
        }

        private IEnumerable<Article> ArticlesFromApi()
        {
            var result = _clientProvider.ExecuteRequest(c => c.GetTopHeadlines(new TopHeadlinesRequest
            {
                Page = 1,
                PageSize = 20,
                Category = Categories.Business,
                Country = Countries.GB,
                Language = Languages.EN,
                Q = ""
            }));

            if (result.Articles == null) return Enumerable.Empty<Article>();

            return result.Articles.Select(ToArticle).ToList();
        }


        private static IEnumerable<Article> TestArticles()
        {
            return Enumerable.Range(1, 5).Select(i => new Article
            {
                Title = "Article " + i,
                Description = "This is about article " + i,
                Url = "#",
                ImageUrl = "https://via.placeholder.com/350x200"
            });
        }

        private static Article ToArticle(NewsApiArticle article)
        {
            return new Article
            {
                Title = article.Title,
                Description = article.Description,
                Url = article.Url,
                ImageUrl = article.UrlToImage,
                PublishDate = article.PublishedAt?.ToLongDateString() ?? string.Empty
            };
        }
    }
}
