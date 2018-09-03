using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public NewsService(INewsApiClientProvider clientProvider)
        {
            _clientProvider = clientProvider;
        }

        public IEnumerable<Article> SearchArticles()
        {

            // TODO: Use a call layer and do not call directly from client provider
            // TODO: Use your own config type and map to TopHeadlinesRequest in call layer
            var result = _clientProvider.ExecuteRequest(c => c.GetTopHeadlines(new TopHeadlinesRequest
            {
                Page = 1,
                PageSize = 20,
                Category = Categories.Technology,
                Country = Countries.GB,
                Language = Languages.EN,
                Q = "Google"
            }));

            if (result.Articles == null) return Enumerable.Empty<Article>();

            return result.Articles.Select(ToArticle).ToList();
        }

        private static Article ToArticle(NewsApiArticle article)
        {
            return new Article
            {
                Title = article.Title,
                Description = article.Description,
                Url = article.Url,
                ImageUrl = article.UrlToImage
            };
        }
    }
}
