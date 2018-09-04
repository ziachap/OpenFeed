using System;
using Microsoft.Extensions.Caching.Memory;
using OpenFeed.Services.DateTimeProvider;

namespace OpenFeed.Services.Cache
{
    public class MemoryCacheWrapper : ICache
    {
        private readonly IMemoryCache _cache;
        private readonly IDateTimeProvider _timeProvider;

        public MemoryCacheWrapper(IMemoryCache cache, IDateTimeProvider timeProvider)
        {
            _cache = cache;
            _timeProvider = timeProvider;
        }

        public bool Exists(string key) => _cache.TryGetValue(key, out _);

        public T Get<T>(string key) => _cache.Get<T>(key);

        public void Set(string key, object value, TimeSpan duration)
        {
            _cache.Set(key, value, _timeProvider.Now() + duration);
        }
    }
}