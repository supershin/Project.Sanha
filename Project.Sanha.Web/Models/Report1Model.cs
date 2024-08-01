using System;
namespace Project.Sanha.Web.Models
{
	public class Report1Model
	{
        public string ShopName { get; set; }
		public string OrderNO { get; set; }
		public string ProjectName { get; set; }
		public string AddrNO { get; set; }
		public string TransferDate { get; set; }
		public int UsedQuota { get; set; }
		public int Status { get; set; }
		public string StatusDesc { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string RelationShip { get; set; }
        public string ImageSignCustomer { get; set; }
        public string DateSignCustomer { get; set; }
        public string StaffName { get; set; }
        public string StaffMobile { get; set; }
        public string WorkDate { get; set; }
        public string WorkTime { get; set; }
        public string Remark { get; set; }
        public string ImageSignStaff { get; set; }
        public string DateSignStaff { get; set; }
    }
}

