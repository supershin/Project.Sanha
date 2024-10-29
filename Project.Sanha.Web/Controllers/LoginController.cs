using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Sanha.Web.Common;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Services;
using QRCoder;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Sanha.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenService _authenService;

        public LoginController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        // GET: /<controller>/
        public IActionResult Index(string param)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            try
            {
                var hash_password = ComputeSha256Hash("ASW" + password.Trim());

                LoginResp userProfile = _authenService.VerifyLogin(userName, hash_password);

                // Throw exception if user profile is null (login failed)
                if (userProfile == null)
                {
                    throw new Exception("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง"); // Custom exception message
                }

                HttpContext.Session.SetString("SAN.ID", userProfile.UserID.ToString());
                HttpContext.Session.SetString("SAN.UserName", userProfile.UserName);
                HttpContext.Session.SetString("SAN.Email", userProfile.Email);
                HttpContext.Session.SetString("SAN.FullName", userProfile.FullName);

                if(userProfile != null)
                {
                    return RedirectToAction("Index", "Approve");
                }
                else
                {
                    throw new Exception("ไม่พบข้อมูลผู้ใช้งาน");
                }
            }
            catch (Exception ex)
            {
                // Use TempData to store the error message and pass it to the view
                TempData["ErrorMessage"] = ex.Message;
                return View("Index");
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("SAN.ID", "");
            HttpContext.Session.SetString("SAN.UserName", "");
            HttpContext.Session.SetString("SAN.Email", "");
            HttpContext.Session.SetString("SAN.FullName", "");

            return RedirectToAction("Index","Login");
        }
    }
}

