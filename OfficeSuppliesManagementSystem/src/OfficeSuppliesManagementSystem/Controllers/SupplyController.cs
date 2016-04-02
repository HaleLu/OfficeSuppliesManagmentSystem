using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using OfficeSuppliesManagementSystem.Models;
using OfficeSuppliesManagementSystem.Utilities;
using OfficeSuppliesManagementSystem.ViewModels.Supply;
using Microsoft.AspNet.Http;

namespace OfficeSuppliesManagementSystem.Controllers
{
    [Authorize]
    public class SupplyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationEnvironment _appEnv;
        public SupplyController(ApplicationDbContext context, IApplicationEnvironment appEnv)
        {
            _context = context;
            _appEnv = appEnv;
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
            return View(Mapper.Map<EditViewModel>(supply));
        }

        // POST: Supply/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var supply = Mapper.Map<Supply>(model);
                var file = Request.Form.Files.GetFile("Photo");
                if (file != null)
                {
                    supply.PhotoUrl = "images\\Supply\\" + model.Id + "." + file.ContentType.Split('/')[1];
                    file.SaveAs(supply.PhotoUrl);
                }
                if (supply != null) _context.Update(supply);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinceSelectItems = typeof (Supply.EProvince).GenerateSelectItems(model.Province);
            ViewBag.TypeSelectItems = typeof (Supply.ESupplyType).GenerateSelectItems(model.Type);
            return View(model);
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