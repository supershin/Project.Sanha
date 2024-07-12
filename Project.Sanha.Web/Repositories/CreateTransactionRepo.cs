using System;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public class CreateTransactionRepo : ICreateTransactionRepo
	{
        private readonly SanhaDbContext _context;

        public CreateTransactionRepo(SanhaDbContext context)
		{
			_context = context;
		}

		public bool CreateTransaction(CreateTransactionModel create)
		{

			return true;
		}
	}
}

