using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;
using OfficeSuppliesManagementSystem.ViewModels.ImportRecord;

namespace OfficeSuppliesManagementSystem.Controllers
{
    public class ImportRecordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportRecordController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ImportRecord
        public IActionResult Index()
        {
            return View(_context.ImportRecord.ToList());
        }

        // GET: ImportRecord/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ImportRecord importRecord = _context.ImportRecord.Single(m => m.Id == id);
            if (importRecord == null)
            {
                return HttpNotFound();
            }

            return View(importRecord);
        }

        // GET: ImportRecord/Create
        public async Task<ViewResult> Create()
        {
            ViewBag.SupplySelectItems = await _context.Supply.Select(s => new SelectListItem
            {
                Text = $"{s.Id} ( {s.Name} )",
                Value = s.Id.ToString()
            }).ToListAsync();
            return View();
        }

        // POST: ImportRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var importRecord = new ImportRecord();
                Mapper.Map(model, importRecord);
#warning !!!
                //                importRecord.Supply = await _context.Supply.FirstOrDefaultAsync(s => s.Id == model.SupplyId);
                importRecord.UnitPrice = model.TotalPrice/model.Quantity;
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

        // GET: ImportRecord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var importRecord = _context.ImportRecord.Single(m => m.Id == id);
            if (importRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplySelectItems = await _context.Supply.Select(s => new SelectListItem
            {
                Text = $"{s.Id} ( {s.Name} )",
                Value = s.Id.ToString()
            }).ToListAsync();
            return View(importRecord);
        }

        // POST: ImportRecord/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ImportRecord importRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Update(importRecord);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplySelectItems = await _context.Supply.Select(s => new SelectListItem
            {
                Text = $"{s.Id} ( {s.Name} )",
                Value = s.Id.ToString()
            }).ToListAsync();
            return View(importRecord);
        }

        // GET: ImportRecord/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ImportRecord importRecord = _context.ImportRecord.Single(m => m.Id == id);
            if (importRecord == null)
            {
                return HttpNotFound();
            }

            return View(importRecord);
        }

        // POST: ImportRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ImportRecord importRecord = _context.ImportRecord.Single(m => m.Id == id);
            _context.ImportRecord.Remove(importRecord);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
