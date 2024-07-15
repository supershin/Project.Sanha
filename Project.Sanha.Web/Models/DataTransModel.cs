using System;
namespace Project.Sanha.Web.Models
{
	public class DataTransModel
	{
		public int EventId { get; set; }
		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerMobile { get; set; }
        public int RelationShip { get; set; }
        public string StaffName { get; set; }
        public int UsingQuota { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Remark { get; set; }
    }
}

