using System;
namespace Project.Sanha.Web.Models
{
	public class InformationDetail
	{
		public string ProjectId { get; set; } = default!;
        public int UnitId { get; set; }
		public string ContractNumber { get; set; } = default!;
        public string ProjectName { get; set; } = default!;
		public string CustomerName { get; set; } = default!;
		public string CustomerMobile { get; set; } = default!;
		public string CustomerEmail { get; set; } = default!;
		public string AddressNo { get; set; } = default!;
		public string? TransferDate { get; set; }

		public List<ShopService>? ListShopService { get; set; }
	}
	public class ShopService
	{
		public int ShopID { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string Exp { get; set; } = default!;
		public int Quota { get; set; }
    }
}

