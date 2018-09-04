using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFeed.Services.Cache
{
    public interface ICache
    {
        T Get<T>(string key);

        void Add(string key, object value, TimeSpan duration);
    }

    public class MemoryCache : ICache
    {
        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value, TimeSpan duration)
        {
            throw new NotImplementedException();
        }
    }
}
