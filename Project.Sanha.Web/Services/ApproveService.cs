using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic;
using Project.Sanha.Web.Common;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;

namespace Project.Sanha.Web.Services
{
    public class ApproveService : IApprove
    {
        private readonly SanhaDbContext _context;
       
        public ApproveService( SanhaDbContext context)
        {
            _context = context;
          
           
        }

        public dynamic GetTransList(DTParamModel param, ApproveModel criteria)
        {
            //throw new NotImplementedException();
            var totalRecord = 0;
            bool asc = param.sortDirection.ToUpper().Contains("ASC");
            //variable = (condition) ? expressionTrue :  expressionFalse;
            criteria.strSearch = (criteria.strSearch == null) ? string.Empty :  criteria.strSearch.ToString() ?? string.Empty;

            var query = from st in _context.Sanha_ts_Shopservice_Trans
                        join us in _context.Sanha_tr_UnitShopservice on st.EventID equals us.ID
                        join mu in _context.master_unit on us.UnitID equals mu.id
                        join mp in _context.master_project on mu.project_id equals mp.id.ToString()
                        join mr in _context.master_relation on st.CustomerRelationID equals mr.id
                        join u in _context.users on criteria.JuristicId equals u.id
                        join up in _context.user_projects on u.id equals up.user_id
                        where st.FlagActive == true
                        select new
                        {
                            st.ID,
                            mp.project_name,
                            mu.addr_no,
                            st.CustomerName,                  
                            st.StaffName,
                            st.WorkDate,
                            st.WorkTime,
                            us.Quota,
                            st.UsedQuota,
                            st.Status,
                            st.CreateDate
                        };
            //query = query.Where(e => e.LastSaleOrder.QuotationNumber.Contains(criteria.strSearch)
            //                    || e.LastSaleOrder.ContracNumber.Contains(criteria.strSearch)
            //                    || e.LastSaleOrder.BookingNumber.Contains(criteria.strSearch)
            //                    || e.UnitCode.Contains(criteria.strSearch)
            //                    || e.AddrNo.Contains(criteria.strSearch)
            //                    || e.Build.Contains(criteria.strSearch)
            //                    || e.Floor.Contains(criteria.strSearch)
            //                    || criteria.strSearch == string.Empty
            //                    || criteria.strSearch == null || criteria.strSearch == string.Empty);

            var result = query.Page(param.start, param.length, i => i.CreateDate, param.SortColumnName, asc, out totalRecord);
            param.TotalRowCount = totalRecord;
            return result.AsEnumerable().Select(e => new
            {
                ID = e.ID,
                ProjectName =e.project_name,
                AddrNo = e.addr_no,
                CustomerName = e.CustomerName,
                StaffName = e.StaffName,
                WorkDate = e.WorkDate.ToStringDate(),
                WorkTime = e.WorkTime,
                Quota = e.Quota,
                UsedQuota = e.UsedQuota,
                Status = SystemConstant.Status.Get_Desc((int)e.Status),
                CreateDate = e.CreateDate.ToStringDateTime()

            }).ToList();
        }

        public ReportDetailModel ReportDetail(string ID)
        {
            int id = Int32.Parse(ID);

            ReportDetailModel detail = new ReportDetailModel();

            var query = (from st in _context.Sanha_ts_Shopservice_Trans
                        join us in _context.Sanha_tr_UnitShopservice on st.EventID equals us.ID
                        join mu in _context.master_unit on us.UnitID equals mu.id
                        join mp in _context.master_project on mu.project_id equals mp.id.ToString()
                        join mr in _context.master_relation on st.CustomerRelationID equals mr.id
                        where st.FlagActive == true && st.ID == id
                        select new
                        {
                            st.ID,
                            mp.project_name,
                            mu.addr_no,
                            mu.transfer_date,
                            st.CustomerName,
                            st.CustomerEmail,
                            st.CustomerMobile,
                            mr.name,
                            st.StaffName,
                            st.WorkDate,
                            st.WorkTime,
                            us.Quota,
                            st.UsedQuota,
                            st.Status,
                            st.Remark,
                        }).FirstOrDefault();

            if (query != null)
            {
                var queryImage = (from sr in _context.Sanha_tr_Shopservice_Resource
                                  where sr.TransID == query.ID && sr.FlagActive == true
                                  select new
                                  {
                                      sr.FilePath,
                                      sr.ResourceType
                                  }).ToList();

                CustomerDetail customer = new CustomerDetail()
                {
                    CustomerName = query.CustomerName,
                    CustomerMobile = query.CustomerMobile,
                    CustomerEmail = query.CustomerEmail,
                    RelationShip = query.name,
                };

                StaffDetail staff = new StaffDetail()
                {
                    StaffName = query.StaffName,
                    WorkDate = query.WorkDate.ToStringDate(),
                    WorkTime = query.WorkTime,
                    Remark = query.Remark
                };

                List<Images> images = new List<Images>();

                foreach( var i in queryImage)
                {               
                    if(i.ResourceType == SystemConstant.ResourceType.IMAGE)
                    {
                        Images image = new Images()
                        {
                            ImagePath = i.FilePath
                        };
                        images.Add(image);
                    }
                    if(i.ResourceType == SystemConstant.ResourceType.SIGNCUST)
                    {
                        customer.ImageSignCustomer = i.FilePath;
                    }
                    if(i.ResourceType == SystemConstant.ResourceType.SIGNSTAFF)
                    {
                        staff.ImageSignStaff = i.FilePath;
                    }
                }

                detail = new ReportDetailModel()
                {
                    ID = query.ID,
                    ProjectName = query.project_name,
                    AddrNo = query.addr_no,
                    TransferDate = query.transfer_date?.ToString("dd-MM-yyyy"),
                    Quota = query.Quota,
                    UsedQuota = query.UsedQuota,
                    Status = query.Status,
                    StatusDesc = SystemConstant.Status.Get_Desc((int)query.Status),
                    CustomerDetail = customer,
                    StaffDetail = staff,
                    Images = images
                };
            }
            
            
            return detail;
        }

        public List<SelectListItem> getProjectList(int id)
        {
            var queryProjects = (from u in _context.users
                                 join up in _context.user_projects on u.id equals up.user_id
                                 join mp in _context.master_project on up.project_id equals mp.id
                                 where u.id == id
                                 select new
                                 {
                                     mp.id,
                                     mp.project_name,
                                 }).ToList();

            var selectLists = new List<SelectListItem>();
            selectLists.Add(new SelectListItem
            {
                Value = "0",
                Text = "ทั้งหมด"
            });

            foreach ( var item in queryProjects)
            {
                selectLists.Add(new SelectListItem
                {
                    Value = item.id.ToString(),
                    Text = item.project_name
                });
            }

            return selectLists;
        }
    }
}
