using lab4.Data;
using lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace lab4.Controllers
{
    public class AddressController : Controller
    {
        private readonly DbConnection _db;

        public AddressController(DbConnection db)
        {
            _db = db;
        }

        // GET: AddressController
        public IActionResult Index()
        {
            IEnumerable<Address> addressesList = _db.Addresses;
            return View(addressesList);
        }

        // GET: AddressController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Address address)
        {

            if (ModelState.IsValid)
            {
                await _db.Addresses.AddAsync(address);
                await _db.SaveChangesAsync();
                TempData["success"] = "Address was added successfully";
                return RedirectToAction("Index");
            }

            return View(address);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var addressFromDb = await _db.Addresses.FindAsync(id);

            if (addressFromDb == null)
            {
                return NotFound();
            }

            return View(addressFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Address address)
        {
            if (ModelState.IsValid)
            {
                _db.Addresses.Update(address);
                await _db.SaveChangesAsync();
                TempData["success"] = "Address was updated successfully";
                return RedirectToAction("Index");
            }
            return View(address);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var addressFromDb = await _db.Addresses.FindAsync(id);

            if (addressFromDb == null)
            {
                return NotFound();
            }

            return View(addressFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var addressFromDb = await _db.Addresses.FindAsync(id);

            if (addressFromDb == null)
            {
                return NotFound();
            }
            _db.Addresses.Remove(addressFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Assress was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
