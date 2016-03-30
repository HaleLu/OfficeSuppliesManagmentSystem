using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using OfficeSuppliesManagementSystem.Models;
using OfficeSuppliesManagementSystem.Utilities;

namespace OfficeSuppliesManagementSystem.Controllers
{
    [Authorize]
    public class SupplyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Supply
        public IActionResult Index()
        {
            return View(_context.Supply.ToList());
        }

        // GET: Supply/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var supply = _context.Supply.Single(m => m.Id == id);
            if (supply == null)
            {
                return HttpNotFound();
            }

            return View(supply);
        }

        // GET: Supply/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.ProvinceSelectItems = typeof (Supply.EProvince).GenerateSelectItems();
            ViewBag.TypeSelectItems = typeof (Supply.ESupplyType).GenerateSelectItems();
            return View();
        }

        // POST: Supply/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(Supply supply)
        {
            if (ModelState.IsValid)
            {
                _context.Supply.Add(supply);
                _context.Inventory.Add(new Inventory
                {
                    Supply = supply
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinceSelectItems = typeof (Supply.EProvince).GenerateSelectItems();
            ViewBag.TypeSelectItems = typeof (Supply.ESupplyType).GenerateSelectItems();
            return View(supply);
        }

        // GET: Supply/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var supply = _context.Supply.Single(m => m.Id == id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProvinceSelectItems = typeof (Supply.EProvince).GenerateSelectItems(supply.Province);
            ViewBag.TypeSelectItems = typeof (Supply.ESupplyType).GenerateSelectItems(supply.Type);
            return View(supply);
        }

        // POST: Supply/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(Supply supply)
        {
            if (ModelState.IsValid)
            {
                _context.Update(supply);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinceSelectItems = typeof (Supply.EProvince).GenerateSelectItems(supply.Province);
            ViewBag.TypeSelectItems = typeof (Supply.ESupplyType).GenerateSelectItems(supply.Type);
            return View(supply);
        }

        // GET: Supply/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var supply = _context.Supply.Single(m => m.Id == id);
            if (supply == null)
            {
                return HttpNotFound();
            }

            return View(supply);
        }

        // POST: Supply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
        {
            var supply = _context.Supply.Single(m => m.Id == id);
            _context.Supply.Remove(supply);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}