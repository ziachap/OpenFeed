using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using OpenFeed.Services.NewsRepository;
using OpenFeed.Services.NewsRepository.Sort;
using OpenFeed.Services.NewsService.QueryBuilder;
using OpenFeed.Services.Pagination;

namespace OpenFeed.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly IQueryableArticleRepository _articleRepository;
        private readonly IPaginationService _paginationService;
        private readonly ISortFactory<ArticleData> _sortFactory;
        private readonly INewsFilterBuilder _newsFilterBuilder;
		private const int PageSize = 20;

        public NewsService(IQueryableArticleRepository articleRepository, IPaginationService paginationService, ISortFactory<ArticleData> sortFactory, INewsFilterBuilder newsFilterBuilder)
        {
	        _articleRepository = articleRepository;
	        _paginationService = paginationService;
	        _sortFactory = sortFactory;
	        _newsFilterBuilder = newsFilterBuilder;
        }

        public IPaginatedResults<Article> SearchArticles(NewsSearchConfiguration config)
	    {
		    return _paginationService.Paginate(ArticlesFromApi(config), config.Page, PageSize);
	    }

	    private IEnumerable<Article> ArticlesFromApi(NewsSearchConfiguration config)
	    {
			return _articleRepository.GetMany(Query(config))
		        .Select(ToArticle)
		        .ToList();
        }

	    IMongoQuery<ArticleData> Query(NewsSearchConfiguration config)
	    {
			return new MongoQuery<ArticleData>(
				_newsFilterBuilder.BuildQuery(config),
				ProjectionDefinition(config),
				_sortFactory.Make(config.SortType).AsMongoSortDefintion());
	    }

	    ProjectionDefinition<ArticleData> ProjectionDefinition(NewsSearchConfiguration config)
	    {
		    return true//config.SortType == SortType.RelevancyDescending 
			    ? Builders<ArticleData>.Projection.MetaTextScore("TextMatchScore") 
			    : null;
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