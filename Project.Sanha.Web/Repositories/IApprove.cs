using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
    public interface IApprove
    {
        dynamic GetTransList(DTParamModel param, ApproveModel criteria);

        ReportDetailModel ReportDetail(string ID);

        List<SelectListItem> getProjectList(int id);
    }
}
