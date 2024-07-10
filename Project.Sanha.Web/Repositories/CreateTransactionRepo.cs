using System;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class CreateTransactionRepo : ICreateTransactionRepo
	{
        private readonly TitleDbContext _context;

        public CreateTransactionRepo(TitleDbContext context)
		{
			_context = context;
		}

		public bool CreateTransaction(CreateTransactionModel create)
		{

			return true;
		}
	}
}

