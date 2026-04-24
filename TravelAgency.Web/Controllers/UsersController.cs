using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Application.DTOs.Users;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var branches = await _userService.GetActiveBranchesAsync();
            var suppliers = await _userService.GetActiveSuppliersAsync();

            ViewBag.BranchId = new SelectList(branches, "Id", "Title");
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name");

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUserAsync(createDto);
                return RedirectToAction(nameof(Index));
            }

            var branches = await _userService.GetActiveBranchesAsync();
            var suppliers = await _userService.GetActiveSuppliersAsync();

            ViewBag.BranchId = new SelectList(branches, "Id", "Title");
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name");

            return View(createDto);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateUserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Firstname = user.Firstname,
                BranchId = user.BranchId,
                IsAdmin = user.IsAdmin,
                SupplierId = user.SupplierId
            };

            return View(updateDto);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateUserDto updateDto)
        {
            if (id != updateDto.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(updateDto);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(updateDto);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
