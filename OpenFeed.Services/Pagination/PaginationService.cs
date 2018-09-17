using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenFeed.Services.Pagination
{
	public class PaginationService : IPaginationService
	{
		public IPaginatedResults<T> Paginate<T>(IEnumerable<T> results, int page, int pageSize)
		{
			var totalPages = (int)Math.Ceiling((double) results.Count() / pageSize);

			var resultSubset = results.Skip(page * pageSize).Take(pageSize).ToList();

			return new PaginatedResults<T>(resultSubset, page, pageSize, totalPages);
		}
	}
}