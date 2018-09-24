using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NPoco;
using OpenFeed.Services.NewsRepository.Sort;

namespace OpenFeed.Services.NewsRepository
{
	public class ArticleData : ITextSearchSortable
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement]
		public string Title { get; set; }

		[BsonElement]
		public string Description { get; set; }

		[BsonElement]
		public string Url { get; set; }

		[BsonElement]
		public string ImageUrl { get; set; }

		[BsonElement]
		public DateTime? PublishDate { get; set; }

		[BsonElement]
		public string Author { get; set; }

		[BsonElement]
		public string Source { get; set; }

		[BsonElement]
		public string Category { get; set; }

		[BsonIgnoreIfNull]
		public double? TextMatchScore { get; set; }
	}
}