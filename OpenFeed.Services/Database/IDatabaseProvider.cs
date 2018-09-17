using System;
using System.Collections.Generic;
using System.Text;
using NPoco;

namespace OpenFeed.Services.Database
{
    public interface IDatabaseProvider
    {
	    IDatabase Database();
    }
}
