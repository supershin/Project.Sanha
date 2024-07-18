using System;
namespace Project.Sanha.Web.Models
{
	public class ReportDetailModel
	{
		public int ID { get; set; }
		public string? ProjectName { get; set; }
		public string? AddrNo { get; set; }
		public string? TransferDate { get; set; }
		public int? Quota { get; set; }
		public int? UsedQuota { get; set; }
		public int? Status { get; set; }
		public string StatusDesc { get; set; }
		public CustomerDetail? CustomerDetail { get; set; }
		public StaffDetail? StaffDetail { get; set; }
		public List<Images>? Images { get; set; }
	}
	public class CustomerDetail
	{
		public string CustomerName { get; set; }
		public string CustomerMobile { get; set; }
		public string CustomerEmail { get; set; }
		public string RelationShip { get; set; }
		public string ImageSignCustomer { get; set; }
	}
	public class StaffDetail
	{
		public string StaffName { get; set; }
		public string WorkDate { get; set; }
		public string WorkTime { get; set; }
		public string Remark { get; set; }
		public string ImageSignStaff { get; set; }
	}
	public class Images
	{
		public string ImagePath { get; set; }
	}
}

