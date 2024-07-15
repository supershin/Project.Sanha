using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public interface IInformationRepo
	{
		InformationDetail InfoDetail(string projectId, string? unitId, string? contractNo);

		InformationDetail InfoProjectName(string projectId);

		CreateUnitShopModel createUnitShop(string projectId, string unitId, string contractNo);

		SearchUnitModel ReturnModel(int unitId);
    }
}

