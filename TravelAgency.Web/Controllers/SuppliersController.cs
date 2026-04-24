using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.Interfaces;
using TravelAgency.Application.DTOs.Suppliers;

namespace TravelAgency.Web.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierService.GetAllActiveSuppliersAsync();
            return View(suppliers);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierByIdAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSupplierDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _supplierService.CreateSupplierAsync(createDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createDto);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierByIdAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateSupplierDto
            {
                SupplierId = supplier.SupplierId,
                FullName = supplier.FullName,
                Commision = supplier.Commision,
                SupplierOrder = supplier.SupplierOrder,
                Adreess1 = supplier.Adreess1,
                Phone1 = supplier.Phone1,
                IsActive = supplier.IsActive
            };

            return View(updateDto);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateSupplierDto updateDto)
        {
            if (id != updateDto.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _supplierService.UpdateSupplierAsync(updateDto);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updateDto);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierByIdAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _supplierService.DeleteSupplierAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
