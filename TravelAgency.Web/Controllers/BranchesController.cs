using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.DTOs.Branches;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class BranchesController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllActiveBranchesAsync();
            return View(branches);
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranchByIdAsync(id.Value);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBranchDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _branchService.CreateBranchAsync(createDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createDto);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranchByIdAsync(id.Value);
            if (branch == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateBranchDto
            {
                BranchId = branch.BranchId,
                Title = branch.Title,
                Description = branch.Description,
                BranchOrder = branch.BranchOrder,
                Address = branch.Address,
                Phone = branch.Phone,
                IsActive = branch.IsActive
            };

            return View(updateDto);
        }

        // POST: Branches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateBranchDto updateDto)
        {
            if (id != updateDto.BranchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _branchService.UpdateBranchAsync(updateDto);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updateDto);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _branchService.GetBranchByIdAsync(id.Value);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _branchService.DeleteBranchAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
