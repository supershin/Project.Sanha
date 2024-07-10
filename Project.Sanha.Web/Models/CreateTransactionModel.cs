using System;
namespace Project.Sanha.Web.Models
{
	public class CreateTransactionModel
	{
        public string ProjectId { get; set; }
        public int UnitId { get; set; }
        public int ShopId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string StaffName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Remark { get; set; }

        public List<IFormFile> Images { get; set; }

        public string Sign { get; set; }
        public string SignJM { get; set; }

        public string ApplicationPath { get; set; }
    }
}
    
