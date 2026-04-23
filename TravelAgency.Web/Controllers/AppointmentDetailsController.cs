using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Application.DTOs.AppointmentDetails;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class AppointmentDetailsController : Controller
    {
        private readonly IAppointmentDetailService _appointmentDetailService;

        public AppointmentDetailsController(IAppointmentDetailService appointmentDetailService)
        {
            _appointmentDetailService = appointmentDetailService;
        }

        // GET: AppointmentDetails
        public async Task<IActionResult> Index()
        {
            var details = await _appointmentDetailService.GetAllWithRelationsAsync();
            return View(details);
        }

        // GET: AppointmentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _appointmentDetailService.GetByIdWithRelationsAsync(id.Value);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: AppointmentDetails/Create
        public async Task<IActionResult> Create()
        {
            var appointments = await _appointmentDetailService.GetActiveAppointmentsAsync();
            var branches = await _appointmentDetailService.GetActiveBranchesAsync();

            ViewData["AppointmentId"] = new SelectList(appointments, "Id", "Title");
            ViewData["BranchId"] = new SelectList(branches, "Id", "Title");

            return View();
        }

        // POST: AppointmentDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentDetailDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _appointmentDetailService.CreateAppointmentDetailAsync(createDto);
                return RedirectToAction(nameof(Index));
            }

            var appointments = await _appointmentDetailService.GetActiveAppointmentsAsync();
            var branches = await _appointmentDetailService.GetActiveBranchesAsync();

            ViewData["AppointmentId"] = new SelectList(appointments, "Id", "Title", createDto.AppointmentId);
            ViewData["BranchId"] = new SelectList(branches, "Id", "Title", createDto.BranchId);

            return View(createDto);
        }

        // GET: AppointmentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _appointmentDetailService.GetByIdAsync(id.Value);
            if (detail == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateAppointmentDetailDto
            {
                AppointmentDetailId = detail.AppointmentDetailId,
                AppointmentId = detail.AppointmentId,
                BranchId = detail.BranchId,
                LeaveTime = detail.LeaveTime,
                Price = detail.Price
            };

            var appointments = await _appointmentDetailService.GetActiveAppointmentsAsync();
            var branches = await _appointmentDetailService.GetActiveBranchesAsync();

            ViewData["AppointmentId"] = new SelectList(appointments, "Id", "Title", detail.AppointmentId);
            ViewData["BranchId"] = new SelectList(branches, "Id", "Title", detail.BranchId);

            return View(updateDto);
        }

        // POST: AppointmentDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateAppointmentDetailDto updateDto)
        {
            if (id != updateDto.AppointmentDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _appointmentDetailService.UpdateAppointmentDetailAsync(updateDto);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            var appointments = await _appointmentDetailService.GetActiveAppointmentsAsync();
            var branches = await _appointmentDetailService.GetActiveBranchesAsync();

            ViewData["AppointmentId"] = new SelectList(appointments, "Id", "Title", updateDto.AppointmentId);
            ViewData["BranchId"] = new SelectList(branches, "Id", "Title", updateDto.BranchId);

            return View(updateDto);
        }

        // GET: AppointmentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detail = await _appointmentDetailService.GetByIdWithRelationsAsync(id.Value);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // POST: AppointmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _appointmentDetailService.DeleteAppointmentDetailAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
