using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;

namespace OfficeSuppliesManagementSystem.Controllers
{
    public class InventoryController : Controller
    {
        private ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Inventory
        public IActionResult Index()
        {
            return View(_context.Inventory.ToList());
        }

        // GET: Inventory/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Inventory inventory = _context.Inventory.Single(m => m.Id == id);
            if (inventory == null)
            {
                return HttpNotFound();
            }

            return View(inventory);
        }
        
        // GET: Inventory/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Inventory inventory = _context.Inventory.Single(m => m.Id == id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Update(inventory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }
    }
}
