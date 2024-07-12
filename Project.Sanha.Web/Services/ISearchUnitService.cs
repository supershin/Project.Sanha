using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Services
{
	public interface ISearchUnitService
	{
		SearchUnitModel searchUnitService(string projectId, string address);
	}
}

