using System.Collections.Generic;

namespace OpenFeed.Services.Pagination
{
	public interface IPaginationService
    {
	    IPaginatedResults<T> Paginate<T>(IEnumerable<T> results, int page, int pageSize);
    }
}
