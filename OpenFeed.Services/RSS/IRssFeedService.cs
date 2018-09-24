using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using OpenFeed.Services.NewsService;

namespace OpenFeed.Services.RSS
{
    public interface IRssFeedService
    {
        IEnumerable<Article> RetrieveAllArticles();
    }

    public class RssFeedService : IRssFeedService
    {
        private readonly IRssFeedRepository _rssFeedRepository;

        public RssFeedService(IRssFeedRepository rssFeedRepository)
        {
            _rssFeedRepository = rssFeedRepository;
        }

        public IEnumerable<Article> RetrieveAllArticles()
        {
            var feeds = _rssFeedRepository.Feeds();
            var articles = Enumerable.Empty<Article>();
            using (var wclient = new WebClient())
            {
                foreach (var feed in feeds)
                {
                    var rssData = wclient.DownloadString(feed.Url);
                    var xml = XDocument.Parse(rssData);

					// TODO: Need the rest of the fields
                    articles = articles.Concat(xml.Descendants("item").Select(x => new Article
                        {
                            Title = (string)x.Element("title"),
                            Description = (string)x.Element("description"),
							Source = feed.Title
                        }));
                }
            }

            return articles.ToList();
        }
    }
}
