using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCExamProject.Attributes;
using MVCExamProject.Data;
using MVCExamProject.Models;
using MVCExamProject.Repository.Interfaces;
using MVCExamProject.ViewModel;
using System.Security.Claims;

namespace MVCExamProject.Controllers
{
    public class UsersSignInController : Controller
    {
        ExamContext context;
        IUserRepository userRepository;
        public UsersSignInController(ExamContext _context, IUserRepository _userRepository)
        {
            context = _context;
            userRepository = _userRepository;

        }

        [HandelError]
        public IActionResult Sign_Up()
        {
            return View();
        }
        [HandelError]
        [HttpPost]
        public IActionResult Sign_Up(SignUPUserViewModel userVM)
        {
            User userdata = new User();

            userdata.Name = userVM.Name;
            userdata.Email = userVM.Email;
            userdata.Password = userVM.Password;
            userdata.Age = userVM.Age;
           
            if (ModelState.IsValid)
            {
                userRepository.Insert(userdata);
                userRepository.Save();

                ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userdata.Id.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Email, userdata.Email));
                claims.AddClaim(new Claim(ClaimTypes.Name, userdata.Name));
                claims.AddClaim(new Claim(ClaimTypes.Role, userRepository.GetRole(userdata.Id)));

                ClaimsPrincipal principal = new ClaimsPrincipal(claims);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("index", "Home");        


            }
            return View(userdata);
        }
        public IActionResult Sign_In()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Sign_In(User user)
        {
            if (userRepository.Find(user.Email, user.Password))
            {
               
                User UserAccount = userRepository.GetUserByEmailAndPassword(user.Email, user.Password);

                if (UserAccount.IsAdmin == true)
                {
                    return RedirectToAction("login", "Admin");
                }
                ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserAccount.Id.ToString()));
                claims.AddClaim(new Claim(ClaimTypes.Email, UserAccount.Email));
                claims.AddClaim(new Claim(ClaimTypes.Name, UserAccount.Name));
                claims.AddClaim(new Claim(ClaimTypes.Role, userRepository.GetRole(user.Id)));

                ClaimsPrincipal principal = new ClaimsPrincipal(claims);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("index", "Home");     

            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index", "Home");     
        }
    }
}
