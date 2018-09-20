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

		public ArticleData GetSingle(FilterDefinition<ArticleData> filter, ISort<ArticleData> sort)
		{
			return GetMany(filter, sort).Single();
		}

		public IEnumerable<ArticleData> GetMany(FilterDefinition<ArticleData> filter, ISort<ArticleData> sort)
		{
			return Execute(collection =>
			{
				return collection.Find(filter).Sort(sort.AsMongoSortDefintion()).ToList();
			});
		}

		private void Execute(Action<IMongoCollection<ArticleData>> operation)
		{
			var collection = _databaseProvider.Database().GetCollection<ArticleData>(ArticleCollection);
			operation(collection);
		}

		private T Execute<T>(Func<IMongoCollection<ArticleData>, T> operation)
		{
			var collection = _databaseProvider.Database().GetCollection<ArticleData>(ArticleCollection);
			var result = operation(collection);
			return result;
		}

		private FilterDefinition<ArticleData> IdFilter(ObjectId id) 
			=> Builders<ArticleData>.Filter.Eq(d => d.Id, id);
	}
}