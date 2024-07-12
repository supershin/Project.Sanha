using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public interface ISearchUnitRepo
	{
		SearchUnitModel SearchUnit(string projectId, string address);
	}
}

