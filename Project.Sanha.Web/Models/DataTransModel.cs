using System;
namespace Project.Sanha.Web.Models
{
	public class DataTransModel
	{
        public string ProjectId { get; set; }
        public int UnitId { get; set; }
        public string ProjectName { get; set; }
        public string Address { get; set; }
        public string TransferDate { get; set; }

        public int InfoId { get; set; }
        public int ShopId { get; set; }

		public string CustomerName { get; set; }
		public string CustomerEmail { get; set; }
		public string CustomerMobile { get; set; }
        //public int RelationShip { get; set; }
        //public string StaffName { get; set; }
        public int Quota { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
    }
}

