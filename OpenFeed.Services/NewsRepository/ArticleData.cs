using System;
using NPoco;

namespace OpenFeed.Services.NewsRepository
{
	[TableName("Article")]
	[PrimaryKey("Id")]
	public class ArticleData
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Url { get; set; }

		public string ImageUrl { get; set; }

		public DateTime? PublishDate { get; set; }

		public string Author { get; set; }

		public string Source { get; set; }

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