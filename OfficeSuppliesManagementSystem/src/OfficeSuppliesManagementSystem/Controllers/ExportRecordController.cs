using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;

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
        public IActionResult Index()
        {
            return View(_context.ExportRecord.ToList());
        }

        // GET: ExportRecord/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ExportRecord exportRecord = _context.ExportRecord.Single(m => m.Id == id);
            if (exportRecord == null)
            {
                return HttpNotFound();
            }

            return View(exportRecord);
        }

        // GET: ExportRecord/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExportRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExportRecord exportRecord)
        {
            if (ModelState.IsValid)
            {
                _context.ExportRecord.Add(exportRecord);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exportRecord);
        }

        // GET: ExportRecord/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ExportRecord exportRecord = _context.ExportRecord.Single(m => m.Id == id);
            if (exportRecord == null)
            {
                return HttpNotFound();
            }
            return View(exportRecord);
        }

        // POST: ExportRecord/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExportRecord exportRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Update(exportRecord);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exportRecord);
        }

        // GET: ExportRecord/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ExportRecord exportRecord = _context.ExportRecord.Single(m => m.Id == id);
            if (exportRecord == null)
            {
                return HttpNotFound();
            }

            return View(exportRecord);
        }

        // POST: ExportRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ExportRecord exportRecord = _context.ExportRecord.Single(m => m.Id == id);
            _context.ExportRecord.Remove(exportRecord);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
