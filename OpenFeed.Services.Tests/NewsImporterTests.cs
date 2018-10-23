using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using OpenFeed.Services.NewsManager;
using OpenFeed.Services.NewsRepository;

namespace OpenFeed.Services.Tests
{
	[TestFixture]
	public class NewsImporterTests
	{
		private MockRepository _repository;
		private Mock<IRepository<ArticleData>> _articleRepository;
		private Mock<INewsAggregator> _newsAggregator;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Strict);
			_articleRepository = _repository.Create<IRepository<ArticleData>>();
			_newsAggregator = _repository.Create<INewsAggregator>();
		}
		
		private INewsImporter Importer()
			=> new NewsImporter(_articleRepository.Object, _newsAggregator.Object);

		private void RunImportAll()
		{
			Importer().ImportAll();
		}

		[Test]
		public void ImportAll_Does_Not_Import_An_Existing_Article()
		{
			var result = Enumerable.Empty<ArticleData>();
			_articleRepository
				.Setup(x => x.Insert(It.IsAny<IEnumerable<ArticleData>>()))
				.Callback<IEnumerable<ArticleData>>(x => result = x);

			_articleRepository.Setup(x => x.GetAll()).Returns(new[]
			{
				new ArticleData {Url = "test-1"},
				new ArticleData {Url = "test-3"}
			});

			_newsAggregator.Setup(x => x.AggregateArticles()).Returns(new[]
			{
				new ArticleData {Url = "test-1"},
				new ArticleData {Url = "test-2"},
				new ArticleData {Url = "test-3"}
			});

			RunImportAll();
			
			Assert.That(result.Single().Url == "test-2");
		}
	}
}
