using System;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class SearchUnitRepo : ISearchUnitRepo
	{
        private readonly SanhaDbContext _context;

        public SearchUnitRepo(SanhaDbContext context)
		{
			_context = context;
		}
		public SearchUnitModel SearchUnit(string projectId, string address)
		{
			SearchUnitModel search = null;

			var queryProject = (from mp in _context.master_project.Where(o => o.project_id == projectId)
								select new
								{
									mp.id
								}).FirstOrDefault();

			var query = (from i in _context.master_unit.Where(o => o.project_id == queryProject.id.ToString() && o.addr_no == address)
						 select new
						 {
							 i.project_id,
							 i.unit_id,
							 i.contract_number
						 }).FirstOrDefault();

			if(query != null)
			{
                search = new SearchUnitModel()
                {
                    ProjectId = projectId,
                    UnitId = query.unit_id,
                    ContractNo = query.contract_number
                };
            }
			
			return search;
		}

    }
}

