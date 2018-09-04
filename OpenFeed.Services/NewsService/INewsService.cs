using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace OpenFeed.Services.NewsService
{
    public interface INewsService
    {
        IEnumerable<Article> SearchArticles();
    }
}