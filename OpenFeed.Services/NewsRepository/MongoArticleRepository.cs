using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using OpenFeed.Services.Database;

namespace OpenFeed.Services.NewsRepository
{
	public class MongoArticleRepository : IQueryableArticleRepository
	{
		private readonly IMongoDatabaseProvider _databaseProvider;

		private const string ArticleCollection = "Article";

		public MongoArticleRepository(IMongoDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}

		public void Insert(ArticleData article)
		{
			Execute(collection => collection.InsertOne(article));
		}

		public void Insert(IEnumerable<ArticleData> articles)
		{
			Execute(collection => collection.InsertMany(articles));
		}

		public IEnumerable<ArticleData> GetAll()
		{
			return Execute(collection => collection.AsQueryable());
		}

		public void Update(ArticleData article)
		{
			Execute(collection => collection.ReplaceOne(IdFilter(article.Id), article));
		}

		public void Delete(ArticleData article)
		{
			Execute(collection => collection.DeleteOne(IdFilter(article.Id)));
		}

		public ArticleData GetSingle(IMongoQuery<ArticleData> query)
		{
			return GetMany(query).Single();
		}

		public IEnumerable<ArticleData> GetMany(IMongoQuery<ArticleData> query)
		{
			return Execute(collection =>
			{
				return collection.Find(query.Filter)
					.Project<ArticleData>(query.Projection)
					.Sort(query.Sort)
					.ToList();
			});
		}

		private void Execute(Action<IMongoCollection<ArticleData>> operation)
		{
			operation(Collection());
		}

		private T Execute<T>(Func<IMongoCollection<ArticleData>, T> operation)
		{
			var result = operation(Collection());
			return result;
		}

		private IMongoCollection<ArticleData> Collection() 
			=> _databaseProvider.Database().GetCollection<ArticleData>(ArticleCollection);

		private FilterDefinition<ArticleData> IdFilter(ObjectId id) 
			=> Builders<ArticleData>.Filter.Eq(d => d.Id, id);
	}
}