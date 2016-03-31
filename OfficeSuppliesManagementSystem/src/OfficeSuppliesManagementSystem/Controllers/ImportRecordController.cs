using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;
using OfficeSuppliesManagementSystem.ViewModels.ImportRecord;

namespace OfficeSuppliesManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ImportRecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportRecord
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View(_context.ImportRecord.Include(m => m.Inventory).Include(m => m.Inventory.Supply).ToList());
        }

        // GET: ImportRecord/Details/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var importRecord = _context.ImportRecord.Include(m => m.Inventory).Include(m => m.Inventory.Supply).Single(m => m.Id == id);
            if (importRecord == null)
            {
                return HttpNotFound();
            }

            return View(importRecord);
        }

        // GET: ImportRecord/Create
        [Authorize(Roles = "Administrator")]
        public async Task<ViewResult> Create()
        {
            ViewBag.InventorySelectItems = new List<SelectListItem>();
            (await _context.Inventory.Include(m => m.Supply).OrderBy(m => m.Id).ToListAsync()).ForEach(m => ViewBag.InventorySelectItems.Add(new SelectListItem
            {
                Text = $"{m.Supply.Id} ( 名称：{m.Supply.Name} )",
                Value = m.Id.ToString()
            }));
            return View();
        }

        // POST: ImportRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var importRecord = Mapper.Map<ImportRecord>(model);
                importRecord.Inventory = await _context.Inventory.FirstOrDefaultAsync(m => m.Id == model.InventoryId);
                importRecord.UnitPrice = model.TotalPrice/model.Quantity;
                importRecord.Inventory.Quantity += importRecord.Quantity;
                _context.ImportRecord.Add(importRecord);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplySelectItems = await _context.Supply.Select(s => new SelectListItem
            {
                Text = $"{s.Id} ( {s.Name} )",
                Value = s.Id.ToString()
            }).ToListAsync();
            return View(model);
        }

        // GET: ImportRecord/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var importRecord = _context.ImportRecord.Include(m => m.Inventory).Include(m => m.Inventory.Supply).Single(m => m.Id == id);
            if (importRecord == null)
            {
                return HttpNotFound();
            }

            return View(importRecord);
        }

        // POST: ImportRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
        {
            var importRecord = _context.ImportRecord.Include(m => m.Inventory).Single(m => m.Id == id);
            importRecord.Inventory.Quantity -= importRecord.Quantity;
            _context.ImportRecord.Remove(importRecord);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
