using System;
using System.Transactions;
using Microsoft.VisualBasic.FileIO;
using Project.Sanha.Web.Common;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;

namespace Project.Sanha.Web.Services
{
	public class ServiceUnitSave : IServiceUnitSave
	{
        private readonly ICreateTransactionRepo _createTransaction;
        private readonly IInformationRepo _information;

		public ServiceUnitSave(ICreateTransactionRepo createTransaction,
            IInformationRepo information)
		{
            _createTransaction = createTransaction;
            _information = information;
		}

        public void SaveUnitEquipmentSign(CreateTransactionModel model)
        {
            TransactionOptions option = new TransactionOptions();
            option.Timeout = new TimeSpan(1, 0, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            {
                try
                {
                    // create transaction
                    GetTransModel getTrans = _createTransaction.CreateTransaction(model);

                    // Upload Image and Create Resource
                    _createTransaction.UploadImage(model.Images, getTrans.TransId, model.ApplicationPath);

                    //customer sign resource
                    if (!string.IsNullOrEmpty(model.Sign))
                    {
                        _createTransaction.UploadSignResource(model.Sign, model.ApplicationPath, getTrans.TransId, SystemConstant.ResourceType.SIGNCUST);
                    }
                    //jm sign resource
                    if (!string.IsNullOrEmpty(model.SignJM))
                    {
                        _createTransaction.UploadSignResource(model.SignJM, model.ApplicationPath, getTrans.TransId, SystemConstant.ResourceType.SIGNSTAFF);
                    }
                    
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        public SearchUnitModel UnitModel(int id)
        {
            SearchUnitModel unitModel = _information.ReturnModel(id);

            return unitModel;
        }
    }
}

