using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.DTOs.Account;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loggedUser = await _userService.AuthenticateAsync(model.UserName, model.Password);

            if (loggedUser == null)
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة المرور غير صحيحة");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", loggedUser.UserId);
            HttpContext.Session.SetString("IsAdmin", loggedUser.IsAdmin.ToString());

            if (loggedUser.IsAdmin)
                return RedirectToAction("CreateAdmin", "Tickets");

            return RedirectToAction("CreateNotAdmin", "Tickets");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
