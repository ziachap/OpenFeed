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
		ArticleData GetSingle(IMongoQuery<ArticleData> query);

		IEnumerable<ArticleData> GetMany(IMongoQuery<ArticleData> query);
	}
}
