using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Sanha.Web.Controllers
{
    public class InformationController : BaseController
    {
        private readonly IInformationService _informationService;
        private readonly IServiceUnitSave _serviceUnitSave;
        private readonly ISearchUnitService _searchUnitService;
        private readonly IHostEnvironment _hosting;

        public InformationController(IInformationService informationService,
            IServiceUnitSave serviceUnitSave,
            ISearchUnitService searchUnitService,
            IHostEnvironment hostEnvironment
            )
        {
            _informationService = informationService;
            _serviceUnitSave = serviceUnitSave;
            _searchUnitService = searchUnitService;
            _hosting = hostEnvironment;
        }

        // GET
        [HttpGet]
        public IActionResult Index(string projectid, string? unitid, string? contractno)
        {
            InformationDetail informationDetail = null;
            CreateUnitShopModel createUnitShop = null;
            try
            {
                // landing by have account
                
                if (!string.IsNullOrWhiteSpace(unitid) && !string.IsNullOrWhiteSpace(contractno))
                {
                    
                    // if - first landing to insert data
                    // if - not first to return data 
                    createUnitShop = _informationService.CreateUnitShop(projectid, unitid, contractno);

                    // get data detail for return to page
                    informationDetail = _informationService.InfoDetailService(createUnitShop.ProjectId, createUnitShop.UnitId, createUnitShop.ContractNo);
                }
                // landing by dont have account 
                else
                {
                    informationDetail = _informationService.InfoProjectName(projectid);
                }
            }
            catch( Exception ex)
            {
                return View("ErrorInfo", ex.Message);
            }

            return View(informationDetail);
        }

        [HttpPost]
        public IActionResult UsingCode(UsingCodeModel request)
        {
            //coupon
            return View(request);
        }

        public ActionResult SaveUnitEquipmentSign(CreateTransactionModel model)
        {
            try
            {
                
                //model.ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
                model.ApplicationPath = _hosting.ContentRootPath;

                validateUnitEquipmentSign(model);
                
                _serviceUnitSave.SaveUnitEquipmentSign(model);

                SearchUnitModel returnData = _serviceUnitSave.UnitModel(model.UnitId);

                return Json(new
                {
                    message = "Success Summit Form",
                    success = true,
                    data = returnData
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }) ;
            }

        }

        private void validateUnitEquipmentSign(CreateTransactionModel model)
        {
            if (string.IsNullOrEmpty(model.Sign)
                || string.IsNullOrEmpty(model.SignJM))
                throw new Exception("โปรดระบุลายเซ็นต์");
        }

        [HttpPost]
        public JsonResult SearchUnit(SearchUnitReq req)
        {
            try
            {
                SearchUnitModel searchUnit = _searchUnitService.searchUnitService(req.ProjectId, req.Address);
                if (searchUnit == null) throw new Exception("ไม่พบข้อมูลบ้านเลขที่");

                return Json(
                    new
                    {
                        success = true,
                        data = searchUnit
                    });
            }
            catch(Exception ex)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = ex.Message
                    });
            }
        }

        [HttpPost]
        public ActionResult Cancel(CreateTransactionModel model)
        {
            try
            {
                SearchUnitModel returnData = _serviceUnitSave.UnitModel(model.UnitId);
                if (returnData == null) throw new Exception("ข้อมูลไม่ถูกต้อง");

                return Json(
                    new
                    {
                        success = true,
                        data = returnData
                    });
            }
            catch (Exception ex)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = ex.Message
                    });
            }
        }
    }

}

