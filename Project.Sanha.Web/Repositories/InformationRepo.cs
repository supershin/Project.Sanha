using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project.Sanha.Web.Repositories
{
	public class InformationRepo : IInformationRepo
    {
		private readonly TitleDbContext _context;
		
		public InformationRepo(TitleDbContext context)
		{
			_context = context;
		}

		public InformationDetail InfoDetail(string projectId, string? unitId, string? contractNo)
		{
			InformationDetail lists = new InformationDetail();

            int ProjectId = Int32.Parse(projectId);

            // 1. Query Shopservice
            var query = from i in _context.Sanha_tr_UnitShopservice
                         join u in _context.master_unit on i.ContractNumber equals u.contract_number
                         join p in _context.master_project on ProjectId equals p.id
                         where i.ProjectID == projectId && i.ContractNumber == contractNo
                         select new
                         {
                             i.ProjectID,
                             i.UnitID,
                             i.ContractNumber,
                             u.customer_name,
                             u.customer_mobile,
                             u.customer_email,
                             u.addr_no,
                             u.transfer_date,
                             p.project_name
                         };

            var queryCoupon = ( from p in _context.Sanha_tm_ProjectShopservice
							  join s in _context.Sanha_tm_Shopservice on p.ShopID equals s.ID
							  where p.ProjectID == projectId && p.FlagActive == true
							  select new
							  {
								  p.ID,
								  p.ShopID,
								  p.Quata,
								  p.DefaultStartDate,
								  p.DefaultEndDate,
								  p.ExpireDate,
								  s.Name,
								  s.Description
							  }).ToList();

			List<ShopService> shopServices = new List<ShopService>();
            var info = query.FirstOrDefault();

            foreach (var coupon in queryCoupon)
			{
                ShopService shop = new ShopService
                {
                    ShopID = coupon.ShopID,
                    Name = coupon.Name,
                    Description = coupon.Description,
                    Quota = coupon.Quata ?? 0
                };

                if (info != null)
                {
                    if (coupon.ExpireDate != null)
                    {
                        shop.Exp = info.transfer_date?.AddDays(coupon.ExpireDate.Value).ToString();
                    }
                    else if (coupon.DefaultEndDate != null)
                    {
                        shop.Exp = coupon.DefaultEndDate.ToString();
                    }
                }

                shopServices.Add(shop);
            }

            if (info != null)
            {
                lists = new InformationDetail()
                {
                    ProjectId = info.ProjectID,
                    UnitId = info.UnitID,
                    ContractNumber = info.ContractNumber,
                    ProjectName = info.project_name,
                    CustomerName = info.customer_name,
                    CustomerMobile = info.customer_mobile,
                    CustomerEmail = info.customer_email,
                    AddressNo = info.addr_no,
                    TransferDate = info.transfer_date.Value.ToString("dd-MM-yyyy"),
                    ListShopService = shopServices,
                    CheckFormat = true,
                };
            }

            // 3. Make Resp for return
            return lists;
        }

        public InformationDetail InfoProjectName(string projectId)
        {
            InformationDetail information = new InformationDetail();

            int id = Int32.Parse(projectId);
            var query = (from i in _context.master_project.Where(o => o.id == id)
                        select new
                        {
                            i.id,
                            i.project_name
                        }).FirstOrDefault();

            information = new InformationDetail()
            {
                ProjectId = projectId,
                ProjectName = query.project_name,
                CheckFormat = false
            };

            return information;
        }

        public CreateUnitShopModel createUnitShop(string projectId, string unitId, string contractNo)
        {
            CreateUnitShopModel data = new CreateUnitShopModel();

            var masterUnit = (from mu in _context.master_unit
                             .Where(o => o.project_id == projectId && o.unit_id == unitId && o.contract_number == contractNo)
                              select new
                              {
                                  mu.project_id,
                                  mu.id,
                                  mu.contract_number,
                                  mu.transfer_date
                              }).FirstOrDefault();

            var projectShop = (from ps in _context.Sanha_tm_ProjectShopservice
                               .Where(o => o.ProjectID == projectId && o.FlagActive == true)
                               select new
                               {
                                   ps.ID,
                                   ps.ProjectID,
                                   ps.DefaultStartDate,
                                   ps.DefaultEndDate,
                                   ps.ExpireDate
                               }).FirstOrDefault(); // can your edit to list if ProjectShop of ProjectId > 1

            var unitShopservice = (from us in _context.Sanha_tr_UnitShopservice
                                   .Where(o => o.ProjectID == masterUnit.project_id && o.ContractNumber == masterUnit.contract_number)
                                   select new
                                   {
                                       us.ProjectID,
                                       us.UnitID,
                                       us.ContractNumber
                                   }).FirstOrDefault();

            if(unitShopservice == null)
            {
                Sanha_tr_UnitShopservice createUnitShopservice = new Sanha_tr_UnitShopservice();
                createUnitShopservice.ProjectID = masterUnit.project_id;
                createUnitShopservice.UnitID = masterUnit.id;
                createUnitShopservice.ShopID = projectShop.ID;
                createUnitShopservice.ContractNumber = masterUnit.contract_number;
                if (projectShop.ExpireDate > 0 || projectShop.ExpireDate != null)
                {
                    createUnitShopservice.StartDate = masterUnit.transfer_date;
                    createUnitShopservice.EndDate = masterUnit.transfer_date?.AddDays((int)projectShop.ExpireDate);
                }
                else
                {
                    createUnitShopservice.StartDate = projectShop.DefaultStartDate;
                    createUnitShopservice.EndDate = projectShop.DefaultEndDate;
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

            return data;
        }
    }
}

