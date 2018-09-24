using System.Collections.Generic;
using System.Linq;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.NewsManager
{
    public interface INewsImporter
    {
	    void ImportAll();
    }

	public class NewsImporter : INewsImporter
	{
		private readonly INewsAggregator _newsAggregator;
		private readonly IRepository<ArticleData> _repository;

		public NewsImporter(IRepository<ArticleData> repository, INewsAggregator newsAggregator)
		{
			_repository = repository;
			_newsAggregator = newsAggregator;
		}

		public void ImportAll()
		{
			_repository.Insert(ArticlesFromApi());
		}

		private IEnumerable<ArticleData> ArticlesFromApi()
		{
			var existingArticles = _repository.GetAll();

			var result = _newsAggregator.AggregateArticles()
				.Where(x => DoesNotExist(x, existingArticles))
				.ToList();

			return result;
		}

		private bool DoesNotExist(ArticleData article, IEnumerable<ArticleData> existingArticles) 
			=> existingArticles.All(x => x.Url != article.Url);
	}
}
