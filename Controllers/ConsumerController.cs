using lab4.Data;
using lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace lab4.Controllers
{
    public class ConsumerController : Controller
    {
        private readonly DbConnection _db;

        public ConsumerController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<Consumer> consumers = _db.Consumers.Include(prop => prop.Address);
            return View(consumers);
        }


        public IActionResult Create()
        {
            ViewBag.Addresses = new SelectList(_db.Addresses, "ID", "Street");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Consumer consumer)
        {
            if (consumer.ID != 0 && consumer.LastName != "" && consumer.Name != "" && consumer.MiddleName != "" && consumer.NumberOfResidents != 0 && consumer.Square != 0 && consumer.AddressID != 0 && consumer.PersonAccount != "")
            {
                await _db.Consumers.AddAsync(consumer);
                await _db.SaveChangesAsync();
                TempData["success"] = "New consumer was added successfully";
                return RedirectToAction("Index");
            }

            ViewBag.Addresses = new SelectList(_db.Addresses, "ID", "Street");

            return View(consumer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var consumerFromDb = _db.Consumers.Include(p => p.Address);

            if (consumerFromDb == null)
            {
                return NotFound();
            }

            return View(consumerFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var consumerFromDb = await _db.Consumers.FindAsync(id);

            if (consumerFromDb == null)
            {
                return NotFound();
            }
            _db.Consumers.Remove(consumerFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Consumer was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
