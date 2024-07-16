using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public interface ICreateTransactionRepo
    {
		GetTransModel CreateTransaction(CreateTransactionModel create);

        bool UploadImage(List<IFormFile> images, int transId, string appPath);

        bool CreateUploadSign(int transId, string fileName, string filePath);

        Resources UploadSignResource(string model, string appPath, int transId);
    }
}

