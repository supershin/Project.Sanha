using System;
namespace Project.Sanha.Web.Models
{
	public class InformationDetail
	{
		public int Id { get; set; }
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

		public bool CheckFormat { get;set; }
	}
	public class ShopService
	{
		public int UnitShopId { get; set; }
		public int ShopID { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string Exp { get; set; } = default!;
		public int? Quota { get; set; }
		public int? Used_Quota { get; set; }
    }
}

