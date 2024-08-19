﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
    public interface IApprove
    {
        dynamic GetTransList(DTParamModel param, ApproveModel criteria);

        ReportDetailModel ReportDetail(string ID);

        List<SelectListItem> getProjectList(int id);

        ReportDetailForApprove DetailGenReport(int transId);

        ApproveTransDetail ReportApprove(ApproveTransModel model);

        bool SaveFilePDF(Guid guid, int transId, string orderNo, string path);

        string? GetPathPDF(int transId);
    }
}
