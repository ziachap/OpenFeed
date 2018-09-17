using System.Collections.Generic;

namespace OpenFeed.Services.Pagination
{
    public interface IPaginatedResults<out T>
	{
		IEnumerable<T> Results { get; }
		int Page { get; }
		int PageSize { get; }
		int TotalPages { get; }
		bool HasNextPage { get; }
		bool HasPreviousPage { get; }
    }

	public class PaginatedResults<T> : IPaginatedResults<T>
	{
		public PaginatedResults(IEnumerable<T> results, int page, int pageSize, int totalPages)
		{
			Results = results;
			Page = page;
			PageSize = pageSize;
			TotalPages = totalPages;
		}

		public IEnumerable<T> Results { get; }
		public int Page { get; }
		public int PageSize { get; }
		public int TotalPages { get; }
		public bool HasNextPage => Page < TotalPages - 1;
		public bool HasPreviousPage => Page > 0;
	}
}
