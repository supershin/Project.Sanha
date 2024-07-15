using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;
using QuestPDF.Infrastructure;
using static System.Net.WebRequestMethods;
using System;
using System.Text.Json;

namespace Project.Sanha.Web.Controllers
{
    public class ApproveController : BaseController
    {
        private readonly IApprove _approve;

        public ApproveController(IApprove approve) 
        {
            _approve = approve;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Detail(string data)
        {
 if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    // ถอดรหัสพารามิเตอร์ JSON
                    var decodedJson = Uri.UnescapeDataString(data);
                    var jsonParam = JsonSerializer.Deserialize<ApproveModel>(decodedJson);

                    // ใช้ข้อมูล JSON ตามต้องการ
                    // ...
                    
                   // return Json(jsonParam);
                }
                catch (Exception ex)
                {
                    // จัดการข้อผิดพลาด
                    return BadRequest("Invalid data parameter");
                }
            }
            return View();
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
    }
}
