using lab4.Data;
using lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: ReceiptController
        private readonly DbConnection _db;

        public ReceiptController(DbConnection db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<Receipt> receipts = _db.Receipts.Include(prop => prop.Service).Include(prop => prop.Consumer).Include(prop => prop.Consumer.Address);
            return View(receipts);
        }


        public IActionResult Create()
        {
            ViewBag.Consumers = new SelectList(_db.Consumers, "ID", "LastName");
            ViewBag.Servises = new SelectList(_db.Services, "ID", "Server");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Receipt receipt)
        {
            if (receipt.ID != 0 && receipt.TypeOfPayment != "" && receipt.DateOfPayment != new DateTime(0001, 1, 1) && receipt.Amount != 0 && receipt.FinalAmount != 0)
            {
                await _db.Receipts.AddAsync(receipt);
                await _db.SaveChangesAsync();
                TempData["success"] = "Receipt was added successfully";
                return RedirectToAction("Index");
            }

            ViewBag.Consumers = new SelectList(_db.Consumers, "ID", "LastName");
            ViewBag.Servises = new SelectList(_db.Services, "ID", "Server");

            return View(receipt);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var receiptFromDb = _db.Receipts.Include(p => p.Consumer).Include(p => p.Consumer.Address).Include(p => p.Service).First(p => p.ID == id);

            if (receiptFromDb == null)
            {
                return NotFound();
            }

            return View(receiptFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var receiptFromDb = await _db.Receipts.FindAsync(id);

            if (receiptFromDb == null)
            {
                return NotFound();
            }
            _db.Receipts.Remove(receiptFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Receipt was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
