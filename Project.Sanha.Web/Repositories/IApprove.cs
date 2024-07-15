using Project.Sanha.Web.Models;

namespace Project.Sanha.Web.Repositories
{
    public interface IApprove
    {
        dynamic GetTransList(DTParamModel param, ApproveModel criteria);
    }
}
