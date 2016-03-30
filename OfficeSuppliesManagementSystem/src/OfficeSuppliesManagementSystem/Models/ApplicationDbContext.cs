using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using OfficeSuppliesManagementSystem.Models;

namespace OfficeSuppliesManagementSystem.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Supply> Supply { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<ExportRecord> ExportRecord { get; set; }
        public DbSet<ImportRecord> ImportRecord { get; set; }
        
    }
}
