namespace OpenFeed.Services.RSS
{
    public class RssFeed
    {
        public RssFeed(string title, string url)
        {
            Title = title;
            Url = url;
        }

        public string Title { get; }
        public string Url { get; }
    }
}