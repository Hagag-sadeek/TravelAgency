using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.DTOs.CustomerCancels;
using TravelAgency.Application.Interfaces;

namespace TravelAgency.Web.Controllers
{
    public class CustomerCancelsController : Controller
    {
        private readonly ICustomerCancelService _customerCancelService;

        public CustomerCancelsController(ICustomerCancelService customerCancelService)
        {
            _customerCancelService = customerCancelService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateCustomerCancelDto createDto)
        {
            if (createDto.CustomerId == 0 || string.IsNullOrEmpty(createDto.Comment))
            {
                return View();
            }

            await _customerCancelService.CreateCustomerCancelAsync(createDto);

            var cancels = await _customerCancelService.GetByCustomerIdAsync(createDto.CustomerId);
            ViewBag.CustomerCancels = cancels;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShowCustomerCancels(int customerId)
        {
            var cancels = await _customerCancelService.GetByCustomerIdAsync(customerId);
            ViewBag.CustomerCancels = cancels;

            return View(nameof(Index));
        }
    }
}
