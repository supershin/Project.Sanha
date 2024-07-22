using System;
namespace Project.Sanha.Web.Models
{
	public class ApproveTransModel
	{
		public int TransID { get; set; }
		public int AuthenID { get; set; }
		public string? Note { get; set; }
		public int Status { get; set; }
	}

	public class ApproveTransDetail
	{
		public int TransID { get; set; }
		public int JuristicID { get; set; }
		public int Status { get; set; }
	}
}

