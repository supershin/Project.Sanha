using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;
using QuestPDF.Infrastructure;
using static System.Net.WebRequestMethods;
using System;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Globalization;
using ClosedXML.Excel;
using System.Text;
using Project.Sanha.Web.Filters;

namespace Project.Sanha.Web.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class ApproveController : BaseController
    {
        private readonly IApprove _approve;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hosting;

        public ApproveController(IApprove approve, IConfiguration configuration, IHostEnvironment hostEnvironment) 
        {
            _approve = approve;
            _configuration = configuration;
            _hosting = hostEnvironment;
        }

        public IActionResult Index()
        {
            int ID = Int32.Parse(Request.Cookies["SAN.ID"]);
            ViewBag.Id = ID;

            GetProjectFromJuristic getProject = new GetProjectFromJuristic()
            {
                SelectProjectLists = _approve.getProjectList(ID)
            };

            return View(getProject);
        }
        [HttpGet]
        public IActionResult Detail(string data)
        {
            ReportDetailModel reportDetail = new ReportDetailModel();
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    // ถอดรหัสพารามิเตอร์ JSON
                    var decodedJson = Uri.UnescapeDataString(data);
                    var jsonParam = JsonSerializer.Deserialize<ApproveModel>(decodedJson);

                    int jurId = Int32.Parse(jsonParam.JuristicId);
                    // ใช้ข้อมูล JSON ตามต้องการ
                    reportDetail = _approve.ReportDetail(jsonParam.ID);
                    reportDetail.JuristicID = jurId;

                   // return Json(jsonParam);
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    return BadRequest("Invalid data parameter");
                }
            }
            return View(reportDetail);
        }
        //public IActionResult Detail(ApproveModel param)
        //{

        //    return View();
        //}
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public JsonResult JsonAjaxGridTransList(DTParamModel param, ApproveModel criteria)
        {
            try
            {
                var resultData = _approve.GetTransList(param, criteria);
                return Json(
                          new
                          {
                              success = true,
                              data = resultData,
                              param.draw,
                              iTotalRecords = param.TotalRowCount,
                              iTotalDisplayRecords = param.TotalRowCount
                          }
                );
            }
            catch (Exception ex)
            {
                return Json(
                            new
                            {
                                success = false,
                                message = ex.Message, //InnerException(ex),
                                data = new[] { ex.Message },
                                param.draw,
                                iTotalRecords = 0,
                                iTotalDisplayRecords = 0
                            }
               );
            }
        }

        public JsonResult ApproveTrans(ApproveTransModel approve)
        {
            try
            {
                if (approve == null) throw new Exception();

                ApproveTransDetail transDetail = _approve.ReportApprove(approve);

                return Json(
                          new
                          {
                              success = true,
                              data = transDetail,
                          }
                );
            }
            catch(Exception ex)
            {
                return Json(
                            new
                            {
                                success = false,
                                message = ex.Message,
                                data = new[] { ex.Message },
                            }
               );
            }
        }

        public JsonResult ExcelUnitMaster(int juristicID, int projectID, int status, string? validFrom, string? validThrough)
        {
            try
            {
                CultureInfo provider = new CultureInfo("en-US");
                Guid myuuid = Guid.NewGuid();
                string myuuidAsString = myuuid.ToString();
                using (var workbook = new XLWorkbook())
                {
                    #region
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                    worksheet.Cell("A1").Value = "โครงการ";
                    worksheet.Cell("B1").Value = "ยูนิต";
                    worksheet.Cell("C1 ").Value = "ชื่อลูกค้า";
                    worksheet.Cell("D1").Value = "ชื่อพนักงาน";
                    worksheet.Cell("E1").Value = "วันที่เข้าทำงาน";
                    worksheet.Cell("F1").Value = "เวลาที่เข้าทำงาน";
                    worksheet.Cell("G1").Value = "จำนวนสิทธิ์ที่ใช้งาน";
                    worksheet.Cell("H1").Value = "หมายเลขเอกสาร";
                    worksheet.Cell("I1").Value = "สถานะ";
                    worksheet.Cell("J1").Value = "ผู้อนุมัติ";
                    worksheet.Cell("K1").Value = "หมายเหตุ";  //วันที่ส่งมอบ deliver_on";
                    worksheet.Cell("L1").Value = "วันที่สร้างรายการ";    //"ประกันโครงสร้าง";

                    #endregion

                    string strConn = _configuration.GetConnectionString("AFSConn");

                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();

                        string query = @"
                                    SELECT
                                        up.user_id,
                                        st.ID AS TransID,
                                        mp.project_name AS ProjectName,
                                        mu.addr_no AS UnitNO,
                                        st.CustomerName AS CustomerName,
                                        st.StaffName AS StaffName,
                                        st.WorkDate AS WorkDate,
                                        st.WorkTime AS WorkTime,
                                        st.UsedQuota AS UsedQuota,
                                        CASE st.Status
                                            WHEN 0 THEN 'ทั้งหมด'
                                            WHEN 1 THEN 'รออนุมัติ'
                                            WHEN 2 THEN 'อนุมัติ'
                                            WHEN 3 THEN 'ไม่อนุมัติ'
                                            WHEN 5 THEN 'เช็คอิน'
                                            ELSE 'Unknown Status'
                                        END AS StatusDescription,
                                        at.OrderNo AS OrderDocumentNO,
                                        at.Note AS Remark,
                                        at.UpdateBy AS UpdateBy,
                                        CONVERT(DATE, CONVERT(VARCHAR, st.CreateDate, 23)) AS CreateDate
                                    FROM
                                        Sanha_ts_Shopservice_Trans AS st
                                    JOIN
                                        Sanha_tr_UnitShopservice AS us ON st.EventID = us.ID
                                    JOIN
                                        master_unit AS mu ON us.UnitID = mu.id
                                    JOIN
                                        master_project AS mp ON mu.project_id = mp.id
                                    JOIN
                                        master_relation AS mr ON st.CustomerRelationID = mr.id
                                    JOIN
                                        users AS u ON u.id = @juristicId
                                    JOIN
                                        user_projects AS up ON mp.id = up.project_id AND up.user_id = u.id
                                    LEFT JOIN
                                        Sanha_ts_Approve_Trans AS at ON st.ID = at.TransID
                                    WHERE
                                        st.FlagActive = 1
                                        AND up.user_id = @juristicId
                                    ";
                        string addQuery = "";
                        if (projectID > 0)
                        {
                            addQuery = " AND mp.id = @projectId";
                        }
                        if (status > 0)
                        {
                            addQuery += " AND st.Status = @status";
                        }
                        if (validFrom != null)
                        {
                            addQuery += " AND st.WorkDate >= @validFrom";
                        }
                        if (validThrough != null)
                        {
                            
                            addQuery += " AND st.WorkDate <= @validThrough";
                        }

                        addQuery += " ORDER BY st.CreateDate DESC;";

                        string queryString = query + addQuery;

                        using (SqlCommand cmd = new SqlCommand(queryString, conn))
                        {
                            cmd.Parameters.AddWithValue("@juristicId", juristicID);
                            if (projectID > 0)
                            {
                                cmd.Parameters.AddWithValue("@projectId", projectID);
                            }
                            if (status > 0)
                            {
                                cmd.Parameters.AddWithValue("@status", status);
                            }
                            if (validFrom != null)
                            {
                                DateTime originalDateTime = DateTime.Parse(validFrom);
                                DateTime newDate = new DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, 0, 0, 0);

                                cmd.Parameters.AddWithValue("@validFrom", newDate);
                            }
                            if (validThrough != null)
                            {
                                DateTime originalDateTime = DateTime.Parse(validThrough);
                                DateTime newDate = new DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, 23, 59, 59);

                                cmd.Parameters.AddWithValue("@validThrough", newDate);
                            }

                            int irow = 1;
                            using (SqlDataReader dr_data = cmd.ExecuteReader())
                            {
                                while (dr_data.Read())
                                {
                                    irow++;
                                    worksheet.Cell("A" + irow).Value = dr_data["ProjectName"].ToString();
                                    worksheet.Cell("B" + irow).Value = "'" + dr_data["UnitNO"].ToString();
                                    worksheet.Cell("C" + irow).Value = dr_data["CustomerName"].ToString();
                                    worksheet.Cell("D" + irow).Value = "'" + dr_data["StaffName"].ToString();
                                    worksheet.Cell("E" + irow).Value = dr_data["WorkDate"].ToString();
                                    worksheet.Cell("F" + irow).Value = dr_data["WorkTime"].ToString();
                                    worksheet.Cell("G" + irow).Value = dr_data["UsedQuota"].ToString();
                                    worksheet.Cell("H" + irow).Value = dr_data["OrderDocumentNO"].ToString();
                                    worksheet.Cell("I" + irow).Value = dr_data["StatusDescription"].ToString();
                                    worksheet.Cell("J" + irow).Value = dr_data["UpdateBy"].ToString();
                                    worksheet.Cell("K" + irow).Value = dr_data["Remark"].ToString();
                                    worksheet.Cell("L" + irow).Value = dr_data["CreateDate"].ToString();
                                }
                            }
                        }
                        conn.Close();
                    }
                    DateTime date = DateTime.Now;
                    string dateFormat = date.ToString("ddMMyyyy");
                    string filename = "export_สรุปข้อมูลการให้บริการ_" + dateFormat + ".xlsx";
                    string filefull = _hosting.ContentRootPath + "/Upload/unit/" + filename;
                    workbook.SaveAs(filefull);
                    HttpContext.Response.ContentType = "application/vnd.ms-excel";
                    //return Redirect("/Upload/unit/" + filename);
                    // Return JSON response with the file URL for download
                    string fileUrl = "Upload/unit/" + filename;  // URL for client-side download
                    return Json(new { success = true, fileUrl });
                }
            }
            catch (Exception ex)
            {
                return Json(
                            new
                            {
                                success = false,
                                message = ex.Message,
                                data = new[] { ex.Message },
                            }
               );
            }
        }
    }
}
