using System.Collections.Generic;
using System.Text;

namespace OpenFeed.Services.NewsRepository
{
    public interface IArticleRepository
    {
	    void Insert(ArticleData article);

	    void Insert(IEnumerable<ArticleData> articles);

		IEnumerable<ArticleData> GetAll();

	    void Update(ArticleData article);

	    void Delete(ArticleData article);
	}
}
