using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Sanha.Web.Models
{
	public class CreateTransactionModel
	{

        public int UnitShopId { get; set; }
        public string ProjectId { get; set; }
        public int UnitId { get; set; }
        public int ShopId { get; set; }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public int RelationShip { get; set; }
        [Required]
        public string CustomerMobile { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string StaffName { get; set; }
        [Required]
        public int UsingQuota { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
        [Required]
        public string Remark { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }

        [Required]
        public string Sign { get; set; }
        [Required]
        public string SignJM { get; set; }

        public string ApplicationPath { get; set; }
    }
}
    
