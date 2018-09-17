using System;
using Microsoft.AspNetCore.Mvc;
using OpenFeed.Services.NewsManager;

namespace OpenFeed.Controllers
{
    public class TasksController : Controller
    {
	    private readonly INewsImporter _newsImporter;

	    public TasksController(INewsImporter newsImporter)
	    {
		    _newsImporter = newsImporter;
	    }

	    public IActionResult ImportAll()
        {
	        try
	        {
		        _newsImporter.ImportAll();
		        return Ok();
	        }
	        catch (Exception ex)
	        {
		        throw new TaskException("Task 'Import All' failed", ex);
	        }
        }
    }

	public class TaskException : Exception
	{
		public TaskException(string message, Exception ex) : base(message, ex)
		{
		}
	}
}