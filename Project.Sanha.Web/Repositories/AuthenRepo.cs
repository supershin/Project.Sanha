using System;
using Project.Sanha.Web.Data;

namespace Project.Sanha.Web.Repositories
{
	public interface IAuthenRepo
	{
		int Authentication(string email);
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
	}
}

