﻿using System;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class SearchUnitRepo : ISearchUnitRepo
	{
        private readonly TitleDbContext _context;
        public SearchUnitRepo(TitleDbContext context)
		{
			_context = context;
		}
		public SearchUnitModel SearchUnit(string projectId, string address)
		{
			SearchUnitModel search = null;

			var query = (from i in _context.master_unit.Where(o => o.project_id == projectId && o.addr_no == address)
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
                    ProjectId = query.project_id,
                    UnitId = query.unit_id,
                    ContractNo = query.contract_number
                };
            }
			
			return search;
		}

    }
}

