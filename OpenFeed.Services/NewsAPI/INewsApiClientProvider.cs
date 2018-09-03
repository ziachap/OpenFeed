using System;
using System.Collections.Generic;
using System.Text;
using NewsAPI;

namespace OpenFeed.Services.NewsAPI
{
    public interface INewsApiClientProvider
    {
        T ExecuteRequest<T>(Func<NewsApiClient, T> request);
    }

    public class NewsApiClientProvider : INewsApiClientProvider
    {
        private NewsApiClient Client()
        {
            //TODO - API key from config
            return new NewsApiClient("233ee8e1073f49adbb01526b69b28b2f");
        }

        public T ExecuteRequest<T>(Func<NewsApiClient, T> request)
        {
            try
            {
                return request.Invoke(Client());
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
