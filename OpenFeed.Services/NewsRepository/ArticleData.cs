using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NPoco;

namespace OpenFeed.Services.NewsRepository
{
	[TableName("Article")]
	[PrimaryKey("Id")]
	public class ArticleData
	{
		//[BsonElement]
		//public Guid Id { get; set; }
		
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

		public int? Hash()
		{
			unchecked 
			{
				if (Title != null && Url != null)
				{
					int hash = 17;
					hash = hash * 23 + Title.GetHashCode();
					hash = hash * 23 + Url.GetHashCode();
					return hash;
				}
				return null;
			}
		}
	}
}