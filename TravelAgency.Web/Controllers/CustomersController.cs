using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.DTOs.Customers;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult QuickAdd()
        {
            return View();
        }

        // GET: Customers needing update
        [HttpGet]
        public async Task<IActionResult> updateInfo()
        {
            var customers = await _customerService.GetCustomersNeedingUpdateAsync();
            return View(nameof(Index), customers);
        }

        // GET: Customers already updated
        [HttpGet]
        public async Task<IActionResult> updatedInfo()
        {
            var customers = await _customerService.GetUpdatedCustomersAsync();
            return View(nameof(Index), customers);
        }

        // GET: Customers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllActiveCustomersAsync();
            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDto createDto)
        {
            if (ModelState.IsValid)
            {
                await _customerService.CreateCustomerAsync(createDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createDto);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var updateDto = new UpdateCustomerDto
            {
                CustomerId = customer.CustomerId,
                Code = customer.Code,
                Job = customer.Job,
                FullName = customer.FullName,
                Adreess1 = customer.Adreess1,
                Adreess2 = customer.Adreess2,
                Phone1 = customer.Phone1,
                Phone2 = customer.Phone2,
                Phone3 = customer.Phone3,
                IsActive = customer.IsActive,
                SendWhatsApp = customer.SendWhatsApp,
                NeedUpdate = customer.NeedUpdate,
                Updated = customer.Updated,
                Points = customer.Points
            };

            return View(updateDto);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCustomerDto updateDto)
        {
            if (id != updateDto.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _customerService.UpdateCustomerAsync(updateDto);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(updateInfo));
            }
            return View(nameof(updateInfo));
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerAdmin(QuickAddCustomerDto dto)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(dto.Name) ||
                string.IsNullOrEmpty(dto.Phone) || dto.Phone.Length != 11)
            {
                return RedirectToAction(nameof(QuickAdd));
            }

            await _customerService.QuickAddCustomerAsync(dto);
            return RedirectToAction(nameof(QuickAdd));
        }
    }
}
