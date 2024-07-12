using System;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;

namespace Project.Sanha.Web.Services
{
	public class SearchUnitService : ISearchUnitService
	{
		private readonly ISearchUnitRepo _searchUnitRepo;

		public SearchUnitService(ISearchUnitRepo searchUnitRepo)
		{
			_searchUnitRepo = searchUnitRepo;
		}

		public SearchUnitModel searchUnitService(string projectId, string address)
		{
			SearchUnitModel search = _searchUnitRepo.SearchUnit(projectId, address);
            return search;
		}
    }
}

