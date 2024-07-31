using System;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;

namespace Project.Sanha.Web.Services
{
	public class InformationService : IInformationService
	{
		private readonly IInformationRepo _informationRepo;

		public InformationService(IInformationRepo informationRepo)
		{
			_informationRepo = informationRepo;
		}

		public InformationDetail InfoDetailService(string projectId, string unitId, string contractNo)
		{
			InformationDetail information = _informationRepo.InfoDetail(projectId, unitId, contractNo);

			return information;
		}

        public InformationDetail InfoProjectName(string projectId)
		{
			InformationDetail information = _informationRepo.InfoProjectName(projectId);

			return information;
		}

		public CreateUnitShopModel CreateUnitShop(string projectId, string unitId, string contractNo)
		{
			CreateUnitShopModel createUnitShop = _informationRepo.createUnitShop(projectId, unitId, contractNo);

			return createUnitShop;
        }

		public DataTransModel GetTransDraft(UsingCodeModel model)
		{
			DataTransModel data = _informationRepo.GetTransDraft(model.InfoId, model.ShopId);

			data.ProjectId = model.ProjectId;
			data.UnitId = model.UnitId;
			data.ProjectName = model.ProjectName;
			data.Address = model.Address;
			data.TransferDate = model.TransferDate;
			data.InfoId = model.InfoId;
			data.Quota = model.Quota;
			data.ShopId = model.ShopId;

			return data;
		}
    }
}

