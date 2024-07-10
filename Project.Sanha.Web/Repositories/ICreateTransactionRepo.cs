using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public interface ICreateTransactionRepo
    {
		bool CreateTransaction(CreateTransactionModel create);
	}
}

