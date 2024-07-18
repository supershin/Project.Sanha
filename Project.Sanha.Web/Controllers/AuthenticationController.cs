using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Sanha.Web.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenService _authenService;

        public AuthenticationController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        // GET: /<controller>/
        public IActionResult Index(string email)
        {
            int authen;

            try
            {
                if (string.IsNullOrEmpty(email)) throw new Exception("ข้อมูลอีเมลล์ผิดพลาด");
                authen = _authenService.Authentication(email);

            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return View();
            }

            return RedirectToAction("Index", "Approve", new {id = authen});
        }
    }
}

