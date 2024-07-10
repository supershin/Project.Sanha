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
	}
}

