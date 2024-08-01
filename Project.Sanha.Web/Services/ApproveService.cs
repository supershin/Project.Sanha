using System.Transactions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Crypto;
using Project.Sanha.Web.Common;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;
using static Project.Sanha.Web.Common.SystemConstant;

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

            int jtID = Int32.Parse(criteria.JuristicId);

            var query = from st in _context.Sanha_ts_Shopservice_Trans
                        join us in _context.Sanha_tr_UnitShopservice on st.EventID equals us.ID
                        join mu in _context.master_unit on us.UnitID equals mu.id
                        join mp in _context.master_project on mu.project_id equals mp.id.ToString()
                        join mr in _context.master_relation on st.CustomerRelationID equals mr.id
                        join u in _context.users on jtID equals u.id
                        join up in _context.user_projects on mp.id equals up.project_id
                        join at in _context.Sanha_ts_Approve_Trans on st.ID equals at.TransID into atGroup
                        from at in atGroup.DefaultIfEmpty()
                        where st.FlagActive == true && up.user_id == jtID
                        select new
                        {
                            up.user_id,
                            st.ID,
                            mp.id,
                            mp.project_name,
                            mu.addr_no,
                            st.CustomerName,
                            st.StaffName,
                            st.WorkDate,
                            st.WorkTime,
                            st.UsedQuota,
                            st.Status,
                            at.OrderNo,
                            at.Note,
                            at.UpdateBy,
                            st.CreateDate,
                        };

            if (!string.IsNullOrEmpty(criteria.strSearch))
            {
                query = query.Where(o =>
                    o.project_name.Contains(criteria.strSearch) ||
                    o.CustomerName.Contains(criteria.strSearch) ||
                    o.addr_no.Contains(criteria.strSearch) ||
                    o.StaffName.Contains(criteria.strSearch) ||
                    (o.UpdateBy != null && o.UpdateBy.Contains(criteria.strSearch)) ||
                    (o.OrderNo != null && o.OrderNo.Contains(criteria.strSearch))
                );
            }

            if (criteria.ProjectId > 0)
            {
                query = query.Where(x => x.id == criteria.ProjectId);
            }

            if (criteria.Status > 0)
            {
                query = query.Where(x => x.Status == criteria.Status);
            }

            if (!string.IsNullOrEmpty(criteria.ValidForm) && DateTime.TryParse(criteria.ValidForm, out DateTime validFrom))
            {
                query = query.Where(x => x.WorkDate >= validFrom);
            }

            if (!string.IsNullOrEmpty(criteria.ValidThrough) && DateTime.TryParse(criteria.ValidThrough, out DateTime validThrough))
            {
                query = query.Where(x => x.WorkDate <= validThrough);
            }

            var result = query.Page(param.start, param.length, i => i.CreateDate, param.SortColumnName, asc, out totalRecord);
            param.TotalRowCount = totalRecord;
            return result.AsEnumerable().Select(e => new
            {
                AuthID = e.user_id,
                ID = e.ID,
                ProjectId = e.id,
                ProjectName = e.project_name,
                AddrNo = e.addr_no,
                CustomerName = e.CustomerName,
                StaffName = e.StaffName,
                WorkDate = e.WorkDate.ToStringDate(),
                WorkTime = e.WorkTime,
                UsedQuota = e.UsedQuota,
                Status = e.Status,
                StatusDesc = SystemConstant.Status.Get_Desc((int)e.Status),
                ApproveBy = e.UpdateBy,
                Note = e.Note,
                OrderNo = e.OrderNo,
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
                            st.StaffMobile,
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
                    StaffMobile = query.StaffMobile,
                    WorkDate = query.WorkDate.ToStringDate(),
                    WorkTime = query.WorkTime,
                    Remark = query.Remark
                };

                List<Images> images = new List<Images>();
                List<ImagesCheckIn> imagesCheckIn = new List<ImagesCheckIn>();

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
                    if(i.ResourceType == SystemConstant.ResourceType.CHECKIN)
                    {
                        ImagesCheckIn imageCI = new ImagesCheckIn()
                        {
                            ImageCIPath = i.FilePath
                        };
                        imagesCheckIn.Add(imageCI);
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
                    Images = images,
                    ImagesCheckIn = imagesCheckIn
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

        public ReportDetailForApprove DetailGenReport(int transId)
        {
            ReportDetailForApprove reportDetail = new ReportDetailForApprove();

            var queryReport = (from at in _context.Sanha_ts_Approve_Trans
                               join st in _context.Sanha_ts_Shopservice_Trans on at.TransID equals st.ID
                               join mp in _context.master_project on at.ProjectID equals mp.id
                               join mu in _context.master_unit on at.UnitID equals mu.id
                               join mr in _context.master_relation on st.CustomerRelationID equals mr.id
                               join s in _context.Sanha_tm_Shopservice on at.ShopID equals s.ID
                               where at.TransID == transId && at.FlagActive == true
                               select new
                               {
                                   s.Name,
                                   at.OrderNo,
                                   mp.project_name,
                                   mu.addr_no,
                                   mu.transfer_date,
                                   st.CustomerName,
                                   st.CustomerEmail,
                                   st.CustomerMobile,
                                   mr.name,
                                   st.StaffName,
                                   st.StaffMobile,
                                   st.WorkDate,
                                   st.WorkTime,
                                   st.UsedQuota,
                                   st.Status,
                                   st.Remark
                               }).FirstOrDefault();
                               
            if (queryReport != null)
            {
                var queryImage = (from sr in _context.Sanha_tr_Shopservice_Resource
                                  where sr.TransID == transId && sr.FlagActive == true
                                  select new
                                  {
                                      sr.FilePath,
                                      sr.ResourceType,
                                      sr.CreateDate
                                  }).ToList();

                CustomerDetail customer = new CustomerDetail()
                {
                    CustomerName = queryReport.CustomerName,
                    CustomerMobile = queryReport.CustomerMobile,
                    CustomerEmail = queryReport.CustomerEmail,
                    RelationShip = queryReport.name,
                };

                StaffDetail staff = new StaffDetail()
                {
                    StaffName = queryReport.StaffName,
                    StaffMobile = queryReport.StaffMobile,
                    WorkDate = queryReport.WorkDate.ToStringDate(),
                    WorkTime = queryReport.WorkTime,
                    Remark = queryReport.Remark
                };

                List<Images> images = new List<Images>();
                List<ImagesCheckIn> imagesCheckIn = new List<ImagesCheckIn>();

                foreach (var i in queryImage)
                {
                    if (i.ResourceType == SystemConstant.ResourceType.IMAGE)
                    {
                        Images image = new Images()
                        {
                            ImagePath = i.FilePath
                        };
                        images.Add(image);
                    }
                    if (i.ResourceType == SystemConstant.ResourceType.SIGNCUST)
                    {
                        customer.ImageSignCustomer = i.FilePath;
                        customer.DateSignCustomer = i.CreateDate.ToStringDate();
                    }
                    if (i.ResourceType == SystemConstant.ResourceType.SIGNSTAFF)
                    {
                        staff.ImageSignStaff = i.FilePath;
                        staff.DateSignStaff = i.CreateDate.ToStringDate();
                    }
                    if (i.ResourceType == SystemConstant.ResourceType.CHECKIN)
                    {
                        ImagesCheckIn imageCI = new ImagesCheckIn()
                        {
                            ImageCIPath = i.FilePath
                        };
                        imagesCheckIn.Add(imageCI);
                    }
                }

                reportDetail = new ReportDetailForApprove()
                {
                    ShopName = queryReport.Name,
                    OrderNO = queryReport.OrderNo,
                    ProjectName = queryReport.project_name,
                    AddrNo = queryReport.addr_no,
                    TransferDate = queryReport.transfer_date?.ToString("dd-MM-yyyy"),
                    UsedQuota = queryReport.UsedQuota,
                    Status = queryReport.Status,
                    StatusDesc = SystemConstant.Status.Get_Desc((int)queryReport.Status),
                    CustomerDetail = customer,
                    StaffDetail = staff,
                    Images = images,
                    ImagesCheckIn = imagesCheckIn
                };
            }


            return reportDetail;
        }

        public ApproveTransDetail ReportApprove(ApproveTransModel model)
        {
            TransactionOptions option = new TransactionOptions();
            option.Timeout = new TimeSpan(1, 0, 0);
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, option))
            {
                try
                {
                    ApproveTransDetail transDetail = new ApproveTransDetail();

                    var queryTrans = (from st in _context.Sanha_ts_Shopservice_Trans
                                      join us in _context.Sanha_tr_UnitShopservice on st.EventID equals us.ID
                                      where st.ID == model.TransID && st.FlagActive == true
                                      select new
                                      {
                                          st.ID,
                                          st.EventID,
                                          us.ProjectID,
                                          us.UnitID,
                                          us.ShopID,
                                          st.Status
                                      }).FirstOrDefault();

                    Guid guid = Guid.NewGuid();

                    var queryAuth = (from u in _context.users
                                     where u.id == model.AuthenID && u.is_active == 1
                                     select new
                                     {
                                         u.id,
                                         u.firstname,
                                         u.lastname
                                     }).FirstOrDefault();

                    int num = Int32.Parse(queryTrans.ProjectID);
                    string projectId = _context.master_project.Where(o => o.id == num && o.is_active == 1).Select(o => o.project_id).FirstOrDefault();

                    string unitId = _context.master_unit.Where(o => o.id == queryTrans.UnitID).Select(o => o.unit_id).FirstOrDefault();

                    int countApprove = _context.Sanha_ts_Approve_Trans.Where(o => o.FlagActive == true && o.OrderNo != null).Count();

                    string result = HashHelper.GenerateApproveNumber(countApprove,projectId,unitId);

                    if (queryTrans != null)
                    {
                        // create approve trans
                        Sanha_ts_Approve_Trans approveTrans = new Sanha_ts_Approve_Trans();
                        approveTrans.ID = guid;
                        approveTrans.TransID = queryTrans.ID;
                        approveTrans.ProjectID = Int32.Parse(queryTrans.ProjectID);
                        approveTrans.UnitID = queryTrans.UnitID;
                        approveTrans.ShopID = queryTrans.ShopID;
                        approveTrans.OrderNo = model.Status == SystemConstant.Status.SUCCESS ? result : null ;
                        approveTrans.Status = model.Status;
                        approveTrans.Note = model.Note;
                        approveTrans.FlagActive = true;
                        approveTrans.CreateDate = DateTime.Now;
                        approveTrans.CreateBy = queryAuth.firstname + " " + queryAuth.lastname;
                        approveTrans.UpdateDate = DateTime.Now;
                        approveTrans.UpdateBy = queryAuth.firstname+" "+queryAuth.lastname;

                        _context.Sanha_ts_Approve_Trans.Add(approveTrans);
                        _context.SaveChanges();

                    }

                    // update trans after create approve
                    Sanha_ts_Shopservice_Trans? shopTrans = _context.Sanha_ts_Shopservice_Trans
                                                            .Where(o => o.ID == model.TransID && o.FlagActive == true).FirstOrDefault();

                    if (shopTrans == null) throw new Exception("ไม่พบข้อมูล Transaction");

                    shopTrans.Status = model.Status;
                    shopTrans.UpdateDate = DateTime.Now;
                    shopTrans.UpdateBy = queryAuth.firstname + " " + queryAuth.lastname;

                    _context.Sanha_ts_Shopservice_Trans.Update(shopTrans);
                    _context.SaveChanges();

                    if (model.Status == SystemConstant.Status.REJECT)
                    {
                        Sanha_tr_UnitShopservice? unitShop = _context.Sanha_tr_UnitShopservice
                                                            .Where(o => o.ID == shopTrans.EventID && o.FlagActive == true).FirstOrDefault();

                        unitShop.UsedQuota = unitShop.UsedQuota - shopTrans.UsedQuota;
                        unitShop.UpdateDate = DateTime.Now;

                        _context.Sanha_tr_UnitShopservice.Update(unitShop);
                        _context.SaveChanges();
                    }

                    transDetail = new ApproveTransDetail()
                    {
                        TransID = shopTrans.ID,
                        JuristicID = queryAuth.id,
                        Status = (int)shopTrans.Status
                    };

                    scope.Complete();
                    return transDetail;
                }
                catch ( Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }
            }
            
        }

        public bool SaveFilePDF(Guid guid, int transId, string orderNo, string path)
        {
            Sanha_tr_Shopservice_Resource savePDF = new Sanha_tr_Shopservice_Resource();
            savePDF.ID = guid;
            savePDF.TransID = transId;
            savePDF.ResourceType = SystemConstant.ResourceType.PDF;
            savePDF.FileName = "file - " + orderNo + " - " + guid + ".pdf";
            savePDF.FilePath = path;
            savePDF.MimeType = "file/pdf";
            savePDF.FlagActive = true;
            savePDF.CreateDate = DateTime.Now;
            savePDF.CreateBy = 1;
            savePDF.UpdateBy = 1;
            savePDF.UpdateDate = DateTime.Now;

            _context.Sanha_tr_Shopservice_Resource.Add(savePDF);
            _context.SaveChanges();

            savePDF = _context.Sanha_tr_Shopservice_Resource.Where(o => o.TransID == transId).FirstOrDefault();
            if(savePDF == null) throw new Exception("บันทึกไฟล์ PDF ผิดพลาด");
            return true;
        }

        public string GetPathPDF(int transId)
        {
            Sanha_tr_Shopservice_Resource? getPath = _context.Sanha_tr_Shopservice_Resource.Where(o =>
                                                    o.TransID == transId && o.FlagActive == true
                                                    && o.ResourceType == SystemConstant.ResourceType.PDF).FirstOrDefault();
            if (getPath == null) throw new Exception("หาข้อมูลไฟล์PDFไม่เจอ");

            return getPath.FilePath;
        }
    }
}
