using System;
namespace Project.Sanha.Web.Models
{
	public class InformationRequest
	{
		public Guid ID { get; set; }
		public string Name { get; set; } = default!;
		public string HouseNo { get; set; } = default!;
		public DateTime TransferDate { get; set; }
	}
}

