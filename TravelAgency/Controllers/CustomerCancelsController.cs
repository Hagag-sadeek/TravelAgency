using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Helper;
using TravelAgency.Models;

namespace TravelAgency.Controllers
{
    public class CustomerCancelsController : Controller
    {
        private readonly TravelAgencyContext _context;

        public CustomerCancelsController(TravelAgencyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
          
            return View();

        }

        [HttpPost]

        public async Task<IActionResult> Index([Bind("CustomerId,comment")] CustomerCancels CustomerCancel)

        {
            if (CustomerCancel.CustomerId == 0 || string.IsNullOrEmpty(CustomerCancel.comment))
                return View();


            CustomerCancel.CancelDate = DateTime.Now.Date;

            _context.Add(CustomerCancel);
            await _context.SaveChangesAsync();

            var can = await _context.CustomerCancels.Where(x => x.CustomerId == CustomerCancel.CustomerId).ToListAsync();

            ViewBag.CustomerCancels = can;

            return View();
        }
        
        [HttpPost]

        public async Task<IActionResult> ShowCustomerCancels([Bind("CustomerId")] CustomerCancels CustomerCancel)
        {
            var can = await _context.CustomerCancels.Where(x => x.CustomerId == CustomerCancel.CustomerId).ToListAsync();

            ViewBag.CustomerCancels = can;

            return View(nameof(Index));
        }

        //        #region Commented



        //        [HttpGet]
        //        public async Task<IActionResult> Index()
        //        {
        //            //   return View(await _context.Customers.Where(x => x.IsActive).ToListAsync());
        //            return View();
        //        }

        //        public async Task<IActionResult> Details(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var customers = await _context.Customers
        //                .FirstOrDefaultAsync(m => m.CustomerId == id);
        //            if (customers == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(customers);
        //        }

        //        public IActionResult Create()
        //        {
        //            return View();
        //        }

        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create([Bind("CustomerId,Code,Job,FullName,Adreess1,Adreess2,Phone1,Phone2,Phone3,IsActive")] Customers customers)
        //        {
        //            if (ModelState.IsValid)
        //            {

        //                _context.Add(customers);
        //                await _context.SaveChangesAsync();
        //                return RedirectToAction(nameof(Index));
        //            }
        //            return View(customers);
        //        }

        //        public async Task<IActionResult> Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var customers = await _context.Customers.FindAsync(id);
        //            if (customers == null)
        //            {
        //                return NotFound();
        //            }
        //            return View(customers);
        //        }

        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Code,Job,FullName,Adreess1,Adreess2,Phone1,Phone2,Phone3,IsActive")] Customers customers)
        //        {
        //            if (id != customers.CustomerId)
        //            {
        //                return NotFound();
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {

        //                    _context.Update(customers);
        //                    await _context.SaveChangesAsync();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!CustomersExists(customers.CustomerId))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }
        //            return View(customers);
        //        }

        //        public async Task<IActionResult> Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var customers = await _context.Customers
        //                .FirstOrDefaultAsync(m => m.CustomerId == id);
        //            if (customers == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(customers);
        //        }

        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> DeleteConfirmed(int id)
        //        {
        //            var customers = await _context.Customers.FindAsync(id);
        //            customers.IsActive = false;
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //        private bool CustomersExists(int id)
        //        {
        //            return _context.Customers.Any(e => e.CustomerId == id);
        //        }

        //        [HttpPost]
        //        public IActionResult AddCustomerAdmin(string name, string phone)
        //        {
        //            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(phone) || phone.Length != 11)
        //                return RedirectToAction(nameof(QuickAdd));

        //            var customer = _context.Customers.FirstOrDefault(x => x.Phone1 == phone.Trim() && x.IsActive);
        //            if (customer != null)
        //                customer.FullName = name.Trim();
        //            else
        //                _context.Customers.Add(new Customers() { FullName = name.Trim(), Phone1 = phone.Trim(), IsActive = true });

        //            _context.SaveChanges();
        //            return RedirectToAction(nameof(QuickAdd));
        //        }

        //#endregion
    }
}
