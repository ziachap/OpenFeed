using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFeed.Services.NewsRepository
{
    public interface ISortFactory<T>
    {
	    ISort<T> Make(SortType sortType);
    }

	public class ArticleSortFactory : ISortFactory<ArticleData>
	{
		public ISort<ArticleData> Make(SortType sortType)
		{
			switch (sortType)
			{
				case SortType.PublishDateAscending:
					return new Sort<ArticleData>(a => a.PublishDate, SortDirection.Ascending);
				case SortType.PublishDateDescending:
					return new Sort<ArticleData>(a => a.PublishDate, SortDirection.Descending);
				default:
					throw new Exception("SortType not supported: " + sortType);
			}
		}
	}
}
