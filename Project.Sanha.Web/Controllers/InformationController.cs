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
        
        public InformationController(IInformationService informationService,
            IServiceUnitSave serviceUnitSave)
        {
            _informationService = informationService;
            _serviceUnitSave = serviceUnitSave;
        }

        // GET
        [HttpGet]
        public IActionResult Index(string projectid, string unitid, string contractno)
        {
            InformationDetail informationDetail = _informationService.InfoDetailService(projectid, unitid, contractno);
            return View(informationDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UsingCode(UsingCodeModel request)
        {
            return View(request);
        }

        public ActionResult SaveUnitEquipmentSign(CreateTransactionModel model)
        {
            try
            {
                model.ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;

                validateUnitEquipmentSign(model);
                
                _serviceUnitSave.SaveUnitEquipmentSign(model);
                return Json(new
                {
                    message = "Error Summit Form",
                    success = true
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
    }
}

