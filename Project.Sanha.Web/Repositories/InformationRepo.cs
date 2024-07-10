using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class InformationRepo : IInformationRepo
    {
		private readonly TitleDbContext _context;
		
		public InformationRepo(TitleDbContext context)
		{
			_context = context;
		}

		public InformationDetail InfoDetail(string projectId, string unitId, string contractNo)
		{
			InformationDetail lists = new InformationDetail();

			int UnitId = Int32.Parse(unitId);
            int ProjectId = Int32.Parse(projectId);

            // 1. Query Shopservice
            var query = from i in _context.Sanha_tr_UnitShopservice
                         join u in _context.master_unit on i.ContractNumber equals u.contract_number
                         join p in _context.master_project on ProjectId equals p.id
                         where i.ProjectID == projectId && i.UnitID == UnitId && i.ContractNumber == contractNo
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

            var queryCoupon = ( from p in _context.Sanha_tr_ProjectShopservice
							  join s in _context.Sanha_tm_Shopservice on p.ShopID equals s.ID
							  where p.ProjectID == projectId
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
                    TransferDate = info.transfer_date.Value.ToString("ddMMyyyy"),
                    ListShopService = shopServices,
                };
            }

            // 3. Make Resp for return
            return lists;
        }
    }
}

