using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
	public interface IInformationRepo
	{
		InformationDetail InfoDetail(string projectId, string unitId, string contractNo);
	}
}

