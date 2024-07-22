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

			var queryList = from i in _context.master_unit.Where(o => o.project_id == queryProject.id.ToString())
						 select new
						 {
							 i.project_id,
							 i.unit_id,
							 i.addr_no,
							 i.contract_number,
							 i.unit_status_id,
							 i.transfer_date
						 };

			var queryAddr = queryList.Where(o => o.addr_no == address).FirstOrDefault();
			if (queryAddr == null) throw new Exception("ไม่พบข้อมูลบ้านเลขที่");

			var query2 = queryList.Where(o => o.addr_no == address && o.unit_status_id == "4" && o.transfer_date.ToString() != null).FirstOrDefault();
			if (query2 == null) throw new Exception("บ้านเลขที่นี้ยังไม่ได้ทำการโอน");

			if(query2 != null)
			{
                search = new SearchUnitModel()
                {
                    ProjectId = projectId,
                    UnitId = query2.unit_id,
                    ContractNo = query2.contract_number
                };
            }
			return search;
		}

    }
}

