using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFeed.Services.NewsService
{
    public class Article
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string PublishDate { get; set; }
    }
}
