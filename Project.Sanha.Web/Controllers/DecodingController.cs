using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Common;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Sanha.Web.Controllers
{
    public class DecodingController : BaseController
    {
        public DecodingController()
        {

        }

        public IActionResult Index(string param)
        {
            string value = HashHelper.DecodeFrom64(param);
            var Array = value.Split(':');

            return RedirectToAction("Index", "Information", new { projectid = Array[0], unitid = Array[1], contractno = Array[2] });
        }

        public IActionResult Encrypt(string param)
        {
            string md5Hash = HashHelper.Encrypt(param);

            return View();
        }
    }   
}

