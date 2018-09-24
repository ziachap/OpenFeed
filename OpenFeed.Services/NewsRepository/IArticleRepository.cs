using System.Collections.Generic;
using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository
{
    public interface IArticleRepository
    {
	    void Insert(ArticleData article);

	    void Insert(IEnumerable<ArticleData> articles);

		IEnumerable<ArticleData> GetAll();

	    void Update(ArticleData article);

	    void Delete(ArticleData article);
	}

	// TODO: FilterDefinition is mongo-specific, make more filter type not mongo-specific
    // TODO: This can eventually be merged into IArticleRepository
	public interface IQueryableArticleRepository : IArticleRepository
	{
		ArticleData GetSingle(FilterDefinition<ArticleData> filter, ISort<ArticleData> sort);

		IEnumerable<ArticleData> GetMany(FilterDefinition<ArticleData> filter, ISort<ArticleData> sort);
	}
}
