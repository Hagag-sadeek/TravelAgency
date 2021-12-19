using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models;
using TravelAgency.ViewModels;

namespace TravelAgency.Controllers
{

    public class AccountController : Controller
    {
        private readonly TravelAgencyContext _context;

        public AccountController(TravelAgencyContext context)
        {
            _context = context;
        }

       

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {

            //validate model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var loggedUser = _context.Users.FirstOrDefault(itm => itm.UserName == model.UserName
                                                    && itm.Password == model.Password);

            if (loggedUser == null)
            {
                return RedirectToAction(nameof(Login));
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
            HttpContext.Session.SetString("MenuItems", "");
            return RedirectToAction("login", "Account");
        }

        //pulic async Task<IActionResult> ResetPassword(ResetPasswordViewModel obj)
       // {
       //     if (!ModelState.IsValid)
       //         return View(obj);

       //     var user = await UserManager.FindByEmailAsync(obj.Email);

       //     var result = await UserManager.ResetPasswordAsync(user, obj.Token, obj.Password);
       //     if (result.Succeeded)
       //     {
       //         ViewBag.changePasswordSuccess = _localizer["ChangePasswordSuccess"];
       //         return View();
       //     }
       //     else
       //     {
       //         ModelState.AddModelError("", _localizer["ResetPassError"]);
       //         return View(obj);
       //     }
       // }

        //[HttpGet]
        //public async Task<IActionResult> Profile()
        //{
        //    var appUser = await UserManager.FindByNameAsync(User.Identity.Name);
        //    var model = _mapper.Map<UserCreateUpdateViewModel>(appUser);
        //    return View(model);
        //}

       

    }
}