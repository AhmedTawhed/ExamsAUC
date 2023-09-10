using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCExamProject.Models;
using MVCExamProject.Repository;
using MVCExamProject.Repository.Interfaces;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Configuration;

namespace MVCExamProject.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminService;

        public AdminController(IAdminRepository adminService)
        {
            this.adminService = adminService;
        }
        public IActionResult Login()
        {
            return View("~/Views/Admin/Auth/Login.cshtml");
        }

        [Route("admin/login")]
        [HttpPost]
        public IActionResult Login(User user)

        {
            if (adminService.Find(user.Email, user.Password ))
            {
                User admin = adminService.GetAdmin(user.Email, user.Password);
                if (admin.IsAdmin == true)
                {
                    ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()));
                    claims.AddClaim(new Claim(ClaimTypes.Name, admin.Name));
                    claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

                    ClaimsPrincipal principal = new ClaimsPrincipal(claims);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Dashboard");
                }
                return View("~/Views/Admin/Auth/Login.cshtml");
            }
            return View("~/Views/Admin/Auth/Login.cshtml");

        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }






    }
}
