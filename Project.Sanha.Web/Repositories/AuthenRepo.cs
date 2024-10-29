using System;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public interface IAuthenRepo
	{
		int Authentication(string email);
		LoginResp VerifyLogin(string userName, string password);
    }

	public class AuthenRepo : IAuthenRepo
	{
        private readonly SanhaDbContext _context;

        public AuthenRepo(SanhaDbContext context)
		{
			_context = context;
		}

		public int Authentication(string email)
		{
			var queryUser = (from u in _context.users
                              where u.email == email && u.is_active == 1
                              select new
                              {
                                  u.id,
                              }).FirstOrDefault();

			if (queryUser == null) throw new Exception("ไม่พบข้อมูลอีเมลล์");

			return queryUser.id;
		}

		public LoginResp VerifyLogin(string userName, string password)
		{
			user? user = _context.users
				.Where(o => o.username == userName && o.password == password && o.is_active == 1)
				.FirstOrDefault();

			LoginResp resp = new LoginResp();

            if (user == null) throw new Exception("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
			else
			{
				resp = new LoginResp()
				{
					UserID = user.id,
					UserName = user.username,
					Email = user.email,
					FullName = user.firstname + " " + user.lastname
				};
			}
			return resp;
        }
	}
}

