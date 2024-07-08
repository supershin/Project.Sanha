using System;
namespace Project.Sanha.Web.Models
{
	public class InformationDetail
	{
		public Guid ID { get; set; }
		public string Name { get; set; } = default!;
		public string HouseNo { get; set; } = default!;
		public DateTime TransferDate { get; set; }

		public List<ShopService>? ListShopService { get; set; }
	}
	public class ShopService
	{
		public int ID { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
    }
}

