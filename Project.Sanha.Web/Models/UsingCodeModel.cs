using System;
namespace Project.Sanha.Web.Models
{
	public class UsingCodeModel
	{
        public string ProjectId { get; set; } = default!;
        public int UnitId { get; set; }
        public int ShopId { get; set; }
        public string ContractNumber { get; set; } = default!;
        public string ProjectName { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public string CustomerMobile { get; set; } = default!;
        public string CustomerEmail { get; set; } = default!;
        public string AddressNo { get; set; } = default!;
        public string? TransferDate { get; set; }

        public ShopService ShopService = new ShopService();

    }
}

