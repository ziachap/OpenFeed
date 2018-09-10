using System;
using System.Collections.Generic;
using System.Linq;
using NewsAPI.Constants;
using NewsAPI.Models;
using OpenFeed.Services.NewsAPI;

namespace OpenFeed.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly INewsApiClientProvider _clientProvider;

        public NewsService(INewsApiClientProvider clientProvider)
        {
            _clientProvider = clientProvider;
        }

        public IEnumerable<Article> SearchArticles(NewsSearchConfiguration config)
        {
            return ArticlesFromApi();
        }

        private IEnumerable<Article> ArticlesFromApi()
        {
            // TODO: Use a call layer and do not call directly from client provider
            // TODO: Use your own config type and map to TopHeadlinesRequest in call layer

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
                Source = "Test News"
            });
        }

        private static Article ToArticle(global::NewsAPI.Models.Article article)
        {
            return new Article
            {
                Title = article.Title,
                Description = article.Description,
                Url = article.Url,
                ImageUrl = article.UrlToImage,
                PublishDate = article.PublishedAt?.ToLongDateString() ?? string.Empty,
                Author = article.Author,
                Source = article.Source.Name
            };
        }
    }
}