﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;
using OfficeSuppliesManagementSystem.Utilities;
using OfficeSuppliesManagementSystem.ViewModels.ApplicationUser;

namespace OfficeSuppliesManagementSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: ApplicationUser
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View(_context.ApplicationUser.ToList());
        }

        // GET: ApplicationUser/Details/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ApplicationUser applicationUser = _context.ApplicationUser.Single(m => m.Id == id);
            applicationUser.Gender.GetDisplayName();

            return View(applicationUser);
        }

        // GET: ApplicationUser/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            var applicationUser = await _userManager.FindByIdAsync(id.ToString());
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            ViewBag.GenderSelectItems = typeof(ApplicationUser.EGnder).GenerateSelectItems(applicationUser.Gender);
            return View(Mapper.Map<EditViewModel>(applicationUser));
        }

        // POST: ApplicationUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                Mapper.Map(model, user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GenderSelectItems = typeof(ApplicationUser.EGnder).GenerateSelectItems(model.Gender);
            return View(model);
        }

        // GET: ApplicationUser/Delete/5
        [ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ApplicationUser applicationUser = _context.ApplicationUser.Single(m => m.Id == id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
        {
            ApplicationUser applicationUser = _context.ApplicationUser.Single(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
