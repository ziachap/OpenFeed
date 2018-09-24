using System.Collections.Generic;
using MongoDB.Driver;

namespace OpenFeed.Services.NewsRepository
{
    public interface IRepository<TData>
    {
	    void Insert(TData article);

	    void Insert(IEnumerable<TData> articles);

		IEnumerable<TData> GetAll();

	    void Update(TData article);

	    void Delete(TData article);
	}

	// TODO: FilterDefinition is mongo-specific, make more filter type not mongo-specific
    // TODO: This can eventually be merged into IArticleRepository
	public interface IQueryableArticleRepository : IRepository<ArticleData>
	{
		ArticleData GetSingle(IMongoQuery<ArticleData> query);

		IEnumerable<ArticleData> GetMany(IMongoQuery<ArticleData> query);
	}
}
