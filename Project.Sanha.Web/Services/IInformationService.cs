using System;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Services
{
	public interface IInformationService
	{
        InformationDetail InfoDetailService(string projectId, string unitId, string contractNo);

    }
}

