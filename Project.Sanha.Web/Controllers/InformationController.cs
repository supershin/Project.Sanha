using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Sanha.Web.Controllers
{
    public class InformationController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(Guid Id, String Name, String HouseNo, string TransferDate)
        {
            List<ShopService> shopServices = new List<ShopService>()
            {

            };

            string format = "yyyy-MM-dd HH:mm:ss";
            DateTime dateObject = DateTime.ParseExact(TransferDate, format, CultureInfo.InvariantCulture);

            InformationDetail InfoDetail = new InformationDetail()
            {
                ID = Id,
                Name = Name,
                HouseNo = HouseNo,
                TransferDate = dateObject,
                ListShopService = shopServices
            };

            return View(InfoDetail);
        }
        public IActionResult UsingCode()
        {
            return View();
        }
    }
}

