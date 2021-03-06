﻿using OpenFeed.Services.Pagination;

namespace OpenFeed.Services.NewsService
{
	public interface INewsService
	{
		IPaginatedResults<Article> SearchArticles(NewsSearchConfiguration config);
	}
}