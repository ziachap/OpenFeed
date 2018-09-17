using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenFeed.Services.Pagination;

namespace OpenFeed.Services.Tests
{
	[TestFixture]
	public class PaginationServiceTests
	{
		private const int PageSize = 5;

		private IPaginationService Service() => new PaginationService();

		private IEnumerable<int> TestResults() => Enumerable.Range(1, 20);

		private IPaginatedResults<int> RunPaginate(IEnumerable<int> results, int page) 
			=> Service().Paginate(results, page, PageSize);

		[Test]
		public void HasNextPage_Equals_True_When_Page_Is_Not_Last_Page()
		{
			var paginatedResults = RunPaginate(TestResults(), 0);
			
			Assert.That(paginatedResults.HasNextPage, Is.True);
		}

		[Test]
		public void HasNextPage_Equals_False_When_Page_Is_Last_Page()
		{
			var paginatedResults = RunPaginate(TestResults(), 3);

			Assert.That(paginatedResults.HasNextPage, Is.False);
		}

		[Test]
		public void HasPreviousPage_Equals_True_When_Page_Is_Not_First_Page()
		{
			var paginatedResults = RunPaginate(TestResults(), 3);

			Assert.That(paginatedResults.HasPreviousPage, Is.True);
		}

		[Test]
		public void HasPreviousPage_Equals_False_When_Page_Is_First_Page()
		{
			var paginatedResults = RunPaginate(TestResults(), 0);

			Assert.That(paginatedResults.HasPreviousPage, Is.False);
		}

		[Test]
		public void TotalPages_Equals_Minimum_Pages_Required_To_Contain_Results()
		{
			var paginatedResults = RunPaginate(TestResults(), 0);

			Assert.That(paginatedResults.TotalPages, Is.EqualTo(4));
		}

		[Test]
		public void Paginate_Restricts_Results_To_Correct_Page_And_Page_Size()
		{
			var paginatedResults = RunPaginate(TestResults(), 2);

			Assert.That(paginatedResults.Page, Is.EqualTo(2));
			Assert.That(paginatedResults.PageSize, Is.EqualTo(PageSize));
			Assert.That(paginatedResults.Results.First(), Is.EqualTo(11));
			Assert.That(paginatedResults.Results.Last(), Is.EqualTo(15));
		}
	}
}
