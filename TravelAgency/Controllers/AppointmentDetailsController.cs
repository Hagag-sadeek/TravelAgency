using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class AppointmentDetailsController : Controller
    {
        private readonly TravelAgencyContext _context;

        public AppointmentDetailsController(TravelAgencyContext context)
        {
            _context = context;
        }

        // GET: AppointmentDetails
        public async Task<IActionResult> Index()
        {
            var travelAgencyContext = _context.AppointmentDetails.Include(a => a.Appointment).Include(a => a.Branch);
            return View(await travelAgencyContext.ToListAsync());
        }

        // GET: AppointmentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetails = await _context.AppointmentDetails
                .Include(a => a.Appointment)
                .Include(a => a.Branch)
                .FirstOrDefaultAsync(m => m.AppointmentDetailId == id);
            if (appointmentDetails == null)
            {
                return NotFound();
            }

            return View(appointmentDetails);
        }

        // GET: AppointmentDetails/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Where(x=>x.IsActive), "AppointmentId", "Title");
            ViewData["BranchId"] = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title");
            return View();
        }

        // POST: AppointmentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentDetailId,AppointmentId,BranchId,LeaveTime,Price")] AppointmentDetails appointmentDetails)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(appointmentDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Where(x=>x.IsActive), "AppointmentId", "AppointmentId", appointmentDetails.AppointmentId);
            ViewData["BranchId"] = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "BranchId", appointmentDetails.BranchId);
            return View(appointmentDetails);
        }

        // GET: AppointmentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetails = await _context.AppointmentDetails.FindAsync(id);
            if (appointmentDetails == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "Title", appointmentDetails.AppointmentId);
            ViewData["BranchId"] = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "Title", appointmentDetails.BranchId);
            return View(appointmentDetails);
        }

        // POST: AppointmentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentDetailId,AppointmentId,BranchId,LeaveTime,Price")] AppointmentDetails appointmentDetails)
        {
            if (id != appointmentDetails.AppointmentDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentDetailsExists(appointmentDetails.AppointmentDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Where(x => x.IsActive), "AppointmentId", "AppointmentId", appointmentDetails.AppointmentId);
            ViewData["BranchId"] = new SelectList(_context.Branches.Where(x => x.IsActive), "BranchId", "BranchId", appointmentDetails.BranchId);
            return View(appointmentDetails);
        }

        // GET: AppointmentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentDetails = await _context.AppointmentDetails
                .Include(a => a.Appointment)
                .Include(a => a.Branch)
                .FirstOrDefaultAsync(m => m.AppointmentDetailId == id);
            if (appointmentDetails == null)
            {
                return NotFound();
            }

            return View(appointmentDetails);
        }

        // POST: AppointmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentDetails = await _context.AppointmentDetails.FindAsync(id);
            _context.AppointmentDetails.Remove(appointmentDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentDetailsExists(int id)
        {
            return _context.AppointmentDetails.Any(e => e.AppointmentDetailId == id);
        }
    }
}
