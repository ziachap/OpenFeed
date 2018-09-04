using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFeed.Services.Cache
{
    public interface ICache
    {
        bool Exists(string key);

        T Get<T>(string key);

        void Set(string key, object value, TimeSpan duration);
    }
}
