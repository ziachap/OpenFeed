using System;
using System.Collections.Generic;
using System.Linq;
using NewsAPI.Constants;
using NewsAPI.Models;
using OpenFeed.Services.NewsAPI;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsManager
{
    public interface INewsAggregator
    {
	    IEnumerable<ArticleData> AggregateArticles();
    }

	public class NewsApiNewsAggregator : INewsAggregator
	{
		private readonly INewsApiClientProvider _apiClientProvider;

		private readonly IEnumerable<TopHeadlinesRequest> _configs = Enum.GetValues(typeof(Categories))
			.Cast<Categories>().Select(c =>
				new TopHeadlinesRequest
				{
					Q = string.Empty,
					PageSize = 40,
					Page = 1,
					Country = Countries.GB,
					Category = c,
					Language = Languages.EN
				});

		public NewsApiNewsAggregator(INewsApiClientProvider apiClientProvider)
		{
			_apiClientProvider = apiClientProvider;
		}

		public IEnumerable<ArticleData> AggregateArticles()
		{
			return _configs.Aggregate(Enumerable.Empty<ArticleData>(),
					(current, config) => current.Concat(ApiArticles(config)))
				.ToList();
		}

		private IEnumerable<ArticleData> ApiArticles(TopHeadlinesRequest request)
		{
			var result = _apiClientProvider.ExecuteRequest(c => c.GetTopHeadlines(request));

			if (result.Articles == null) return Enumerable.Empty<ArticleData>();
			
			var category = Enum.GetName(typeof(Categories), request.Category);

			return result.Articles.Select(a => ToArticle(a, category)).ToList();
		}

		private static ArticleData ToArticle(Article article, string category)
		{
			return new ArticleData
			{
				Title = article.Title,
				Description = article.Description,
				Url = article.Url,
				ImageUrl = article.UrlToImage,
				PublishDate = article.PublishedAt,
				Author = article.Author,
				Source = article.Source.Name,
				Category = category
			};
		}
	}
}
