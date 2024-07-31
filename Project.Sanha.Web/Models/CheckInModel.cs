using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Sanha.Web.Models
{
	public class CheckInModel
	{
        public int UnitShopId { get; set; }
        public string ProjectId { get; set; }
        public int UnitId { get; set; }
        public int ShopId { get; set; }

        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }

        public List<IFormFile> Image { get; set; }
    }
}

