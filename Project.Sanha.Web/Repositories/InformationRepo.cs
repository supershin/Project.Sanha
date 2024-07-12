using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project.Sanha.Web.Repositories
{
	public class InformationRepo : IInformationRepo
    {
		private readonly SanhaDbContext _context;
		
		public InformationRepo(SanhaDbContext context)
		{
			_context = context;
		}

		public InformationDetail InfoDetail(string projectId, string? unitId, string? contractNo)
		{
			InformationDetail result = new InformationDetail();

            int ProjectId = Int32.Parse(projectId);
            int UnitId = Int32.Parse(unitId);
        
            // 1. Query Shopservice
            var unitShops = (from u in _context.Sanha_tr_UnitShopservice
                         where u.ProjectID == projectId && u.ContractNumber == contractNo && u.UnitID == UnitId
                         select new
                         {
                             u.ID,
                             u.ProjectID,
                             u.UnitID,
                             u.ContractNumber,
                             u.ShopID,
                             u.EndDate,
                             u.Used_Quota
                         }).ToList();

            List<ShopService> shopServices = new List<ShopService>();

            foreach ( var unit in unitShops)
            {
                var projectShop = (from p in _context.Sanha_tm_ProjectShopservice
                                   join s in _context.Sanha_tm_Shopservice on p.ShopID equals s.ID
                                   join m in _context.Sanha_tm_UnitQuota_Mapping on p.ProjectID equals m.ProjectID
                                   where p.ProjectID == projectId && m.UnitID == UnitId
                                   && p.ShopID == unit.ShopID && p.FlagActive == true
                                   select new
                                   {
                                       p.ID,
                                       p.ShopID,
                                       s.Name,
                                       s.Description,
                                       m.Quota
                                   }).FirstOrDefault();

                if(projectShop != null)
                {
                    Sanha_tm_ProjectShopservice? update = _context.Sanha_tm_ProjectShopservice.Where(o => o.ID == projectShop.ID).FirstOrDefault();
                    if(update != null)
                    {
                        update.Min_Quota = 1;
                        update.Max_Quota = unit.Used_Quota;
                        update.UpdateDate = DateTime.Now;
                        update.UpdateBy = 2;
                        _context.Sanha_tm_ProjectShopservice.Update(update);
                        _context.SaveChanges();
                    }

                    ShopService shop = new ShopService
                    {
                        UnitShopId = unit.ID,
                        ShopID = projectShop.ShopID,
                        Name = projectShop.Name,
                        Description = projectShop.Description,
                        Exp = unit.EndDate?.ToString("dd-MM-yyyy"),
                        Quota = unit.Used_Quota != null ? unit.Used_Quota : projectShop.Quota,
                        Min_Quota = update.Min_Quota,
                        Max_Quota = update.Max_Quota
                    };

                    shopServices.Add(shop);
                }
            }

            var info = (from mu in _context.master_unit
                       join mp in _context.master_project on ProjectId equals mp.id
                       where mu.project_id == projectId && mu.id == UnitId && mu.contract_number == contractNo
                       select new
                       {
                           mu.project_id,
                           mu.id,
                           mu.contract_number,
                           mu.customer_name,
                           mu.customer_mobile,
                           mu.customer_email,
                           mu.addr_no,
                           mu.transfer_date,
                           mp.project_name
                       }).FirstOrDefault();

            if (info != null)
            {
                result = new InformationDetail()
                {
                    ProjectId = info.project_id,
                    UnitId = info.id,
                    ContractNumber = info.contract_number,
                    ProjectName = info.project_name,
                    CustomerName = info.customer_name,
                    CustomerMobile = info.customer_mobile,
                    CustomerEmail = info.customer_email,
                    AddressNo = info.addr_no,
                    TransferDate = info.transfer_date?.ToString("dd-MM-yyyy"),
                    ListShopService = shopServices,
                    CheckFormat = true,
                };
            }

            // 3. Make Resp for return
            return result;
        }

        public InformationDetail InfoProjectName(string projectId)
        {
            InformationDetail information = new InformationDetail();

            var query = (from i in _context.master_project.Where(o => o.project_id == projectId)
                        select new
                        {
                            i.id,
                            i.project_id,
                            i.project_name
                        }).FirstOrDefault();

            information = new InformationDetail()
            {
                Id = query.id,
                ProjectId = query.project_id,
                ProjectName = query.project_name,
                CheckFormat = false
            };

            return information;
        }

        public CreateUnitShopModel createUnitShop(string projectId, string unitId, string contractNo)
        {
            CreateUnitShopModel data = new CreateUnitShopModel();

            var masterProeject = _context.master_project.Where(o => o.project_id == projectId).FirstOrDefault();

            var masterUnit = (from mu in _context.master_unit
                             .Where(o => o.project_id == masterProeject.id.ToString() && o.unit_id == unitId && o.contract_number == contractNo)
                              select new
                              {
                                  mu.project_id,
                                  mu.id,
                                  mu.contract_number,
                                  mu.transfer_date
                              }).FirstOrDefault();

            var projectShop = (from ps in _context.Sanha_tm_ProjectShopservice
                               .Where(o => o.ProjectID == masterUnit.project_id && o.FlagActive == true)
                               select new
                               {
                                   ps.ID,
                                   ps.ProjectID,
                                   ps.ShopID,
                                   ps.Quota,
                                   ps.DefaultStartDate,
                                   ps.DefaultEndDate,
                                   ps.ExpireDate
                               }).ToList();

            if (masterUnit != null)
            {
                foreach( var project in projectShop.ToList())
                {

                    var unitShopservice = (from us in _context.Sanha_tr_UnitShopservice
                                           where us.ProjectID == masterUnit.project_id && us.ContractNumber == masterUnit.contract_number
                                           && us.ShopID == project.ShopID && us.FlagActive == true
                                           select new
                                           {
                                               us.ProjectID,
                                               us.UnitID,
                                               us.ContractNumber
                                           }).FirstOrDefault();

                    var unitMapping = (from um in _context.Sanha_tm_UnitQuota_Mapping
                                       where um.ProjectID == masterUnit.project_id && um.UnitID == masterUnit.id
                                       && um.ShopID == project.ShopID && um.FlagActive == true
                                       select new
                                       {
                                           um.ID,
                                           um.Quota,

                                       }).FirstOrDefault();

                    if (unitShopservice == null)
                    {
                        Sanha_tr_UnitShopservice createUnitShopservice = new Sanha_tr_UnitShopservice();
                        createUnitShopservice.ProjectID = masterUnit.project_id;
                        createUnitShopservice.UnitID = masterUnit.id;
                        createUnitShopservice.ShopID = project.ShopID; // remark -> cant use for one Shop
                        createUnitShopservice.ContractNumber = masterUnit.contract_number;
                        if (project.ExpireDate > 0 || project.ExpireDate != null)
                        {
                            createUnitShopservice.StartDate = masterUnit.transfer_date;
                            createUnitShopservice.EndDate = masterUnit.transfer_date?.AddDays((int)project.ExpireDate);
                        }
                        else
                        {
                            createUnitShopservice.StartDate = project.DefaultStartDate;
                            createUnitShopservice.EndDate = project.DefaultEndDate;
                        }

                        if(unitMapping != null)
                        {
                            createUnitShopservice.Used_Quota = createUnitShopservice.EndDate <= DateTime.Now ? 0 : unitMapping.Quota;
                        }
                        else
                        {
                            createUnitShopservice.Used_Quota = createUnitShopservice.EndDate <= DateTime.Now ? 0 : project.Quota;
                        }
                        createUnitShopservice.FlagActive = true;
                        createUnitShopservice.CreateDate = DateTime.Now;
                        createUnitShopservice.CreateBy = 1;
                        createUnitShopservice.UpdateDate = DateTime.Now;
                        createUnitShopservice.UpdateBy = 1;

                        _context.Sanha_tr_UnitShopservice.Add(createUnitShopservice);
                        _context.SaveChanges();

                        data = new CreateUnitShopModel()
                        {
                            ProjectId = createUnitShopservice.ProjectID,
                            UnitId = createUnitShopservice.UnitID.ToString(),
                            ContractNo = createUnitShopservice.ContractNumber
                        };
                    }
                    else
                    {
                        data = new CreateUnitShopModel()
                        {
                            ProjectId = unitShopservice.ProjectID,
                            UnitId = unitShopservice.UnitID.ToString(),
                            ContractNo = unitShopservice.ContractNumber
                        };
                    }
                }               
            }
            return data;
        }
    }
}

