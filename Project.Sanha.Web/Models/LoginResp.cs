using System;
namespace Project.Sanha.Web.Models
{
	public class LoginResp
	{
		public int UserID { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? FullName { get; set; }
	}
}

