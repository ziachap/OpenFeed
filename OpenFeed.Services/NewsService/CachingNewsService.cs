using System;
using System.Collections.Generic;
using OpenFeed.Services.Cache;
using OpenFeed.Services.Pagination;

namespace OpenFeed.Services.NewsService
{
    public class CachingNewsService<T> : INewsService where T : INewsService
    {
        private readonly ICache _cache;
        private readonly T _newsService;
        private const string Key = "_Articles";

        public CachingNewsService(ICache cache, T newsService)
        {
            _cache = cache;
            _newsService = newsService;
        }

        public IPaginatedResults<Article> SearchArticles(NewsSearchConfiguration config)
        {
            // TODO: This is a very basic temporary cache that does not consider search configuration

            if (_cache.Exists(Key))
            {
                return _cache.Get<IPaginatedResults<Article>>(Key);
            }

            var value = _newsService.SearchArticles(config);

            _cache.Set(Key, value, new TimeSpan(1, 0, 0, 0));

            return value;
        }
    }
}