using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;
using OfficeSuppliesManagementSystem.ViewModels.ExportRecord;

namespace OfficeSuppliesManagementSystem.Controllers
{
    [Authorize]
    public class ExportRecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportRecordController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ExportRecord
        [Authorize]
        public IActionResult Index()
        {
            return View(_context.ExportRecord.Include(m => m.Inventory).Include(m => m.Inventory.Supply).ToList());
        }

        // GET: ExportRecord/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var exportRecord = _context.ExportRecord.Include(m => m.Inventory).Include(m => m.Inventory.Supply).Single(m => m.Id == id);
            if (exportRecord == null)
            {
                return HttpNotFound();
            }

            return View(exportRecord);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Accept(int id)
        {
            var exportRecord = _context.ExportRecord.Include(m => m.Inventory).Single(m => m.Id == id);
            exportRecord.Status = ExportRecord.EApplyStatus.Accepted;
            exportRecord.Inventory.Quantity -= exportRecord.Quantity;
            _context.Inventory.Update(exportRecord.Inventory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Reject(int id)
        {
            var exportRecord = _context.ExportRecord.Single(m => m.Id == id);
            exportRecord.Status = ExportRecord.EApplyStatus.Rejected;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ExportRecord/Create
        [Authorize]
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

        // POST: ExportRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exportRecord = Mapper.Map<ExportRecord>(model);
                exportRecord.Status = ExportRecord.EApplyStatus.Pending;
                exportRecord.Inventory = await _context.Inventory.FirstOrDefaultAsync(m => m.Id == model.InventoryId);
                _context.ExportRecord.Add(exportRecord);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InventorySelectItems = new List<SelectListItem>();
            (await _context.Inventory.Include(m => m.Supply).OrderBy(m => m.Id).ToListAsync()).ForEach(m => ViewBag.InventorySelectItems.Add(new SelectListItem
            {
                Text = $"{m.Supply.Id} ( 名称：{m.Supply.Name} )",
                Value = m.Id.ToString()
            }));
            return View(model);
        }
        
        // GET: ExportRecord/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var exportRecord = _context.ExportRecord.Include(m => m.Inventory).Include(m => m.Inventory.Supply).Single(m => m.Id == id);
            if (exportRecord == null)
            {
                return HttpNotFound();
            }

            return View(exportRecord);
        }

        // POST: ExportRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var exportRecord = _context.ExportRecord.Single(m => m.Id == id);
            _context.ExportRecord.Remove(exportRecord);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
