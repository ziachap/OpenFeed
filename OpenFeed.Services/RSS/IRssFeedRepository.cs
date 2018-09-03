using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenFeed.Services.RSS
{
    public interface IRssFeedRepository
    {
        IEnumerable<RssFeed> Feeds();
    }

    //TODO: There should eventually be a db/config version of this repository
    public class RssFeedRepository : IRssFeedRepository
    {
        public IEnumerable<RssFeed> Feeds()
        {
            return new[]
            {
                new RssFeed("ReutersTopNews", "http://feeds.reuters.com/reuters/UKTopNews")
            };
        }
    }
}
