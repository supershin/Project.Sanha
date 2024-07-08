using System;
using Microsoft.EntityFrameworkCore;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class InformationRepo : IInformationRepo
    {
		// context
		public InformationRepo(/*context*/)
		{
			// context 
		}

		public List<ShopService> ListShopServices()
		{
			List<ShopService> lists = new List<ShopService>();

			// 1. Query Shopservice
			var query = ( from i in _context.sh_Shopservice.Where(o => o.FlagActive))
			
            // 2. Query ProjectShopservice
            var query = (from i in _context.sh_ProjectShopservice.Where(o => o.FlagActive))

            // 3. Make Resp for return

            return lists;
        }
    }
}

