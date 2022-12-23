using lab4.Data;
using lab4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace lab4.Controllers
{
    public class ServiceController : Controller
    {
        private readonly DbConnection _db;

        public ServiceController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Service> serviceList = _db.Services;
            return View(serviceList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                await _db.Services.AddAsync(service);
                await _db.SaveChangesAsync();
                TempData["success"] = "Service was added successfully";
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var serviceFromDb = await _db.Services.FindAsync(id);

            if (serviceFromDb == null)
            {
                return NotFound();
            }

            return View(serviceFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                _db.Services.Update(service);
                await _db.SaveChangesAsync();
                TempData["success"] = "Service was updated successfully";
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var serviceFromDb = await _db.Services.FindAsync(id);

            if (serviceFromDb == null)
            {
                return NotFound();
            }

            return View(serviceFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var serviceFromDb = await _db.Services.FindAsync(id);

            if (serviceFromDb == null)
            {
                return NotFound();
            }
            _db.Services.Remove(serviceFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Service was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
