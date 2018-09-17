using System;
using System.Collections.Generic;
using NPoco;
using OpenFeed.Services.Database;

namespace OpenFeed.Services.NewsRepository
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly IDatabaseProvider _databaseProvider;

		public ArticleRepository(IDatabaseProvider databaseProvider)
		{
			_databaseProvider = databaseProvider;
		}
		
		private void Execute(Action<IDatabase> operation)
		{
			using (var db = _databaseProvider.Database())
			{
				operation(db);
			}
		}

		private T Execute<T>(Func<IDatabase, T> operation)
		{
			using (var db = _databaseProvider.Database())
			{
				var result = operation(db);
				return result;
			}
		}

		public void Insert(ArticleData article)
		{
			Execute(db => db.Insert(article));
		}

		public void Insert(IEnumerable<ArticleData> articles)
		{
			Execute(db => db.InsertBulk(articles));
		}

		public IEnumerable<ArticleData> GetAll()
		{
			return Execute(db => db.Fetch<ArticleData>());
		}

		public void Update(ArticleData article)
		{
			Execute(db => db.Update(article));
		}

		public void Delete(ArticleData article)
		{
			Execute(db => db.Delete(article));
		}
	}
}
