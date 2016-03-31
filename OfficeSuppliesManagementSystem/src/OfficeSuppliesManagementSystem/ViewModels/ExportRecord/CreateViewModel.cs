using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OfficeSuppliesManagementSystem.Models;

namespace OfficeSuppliesManagementSystem.ViewModels.ExportRecord
{
    public class CreateViewModel
    {
        [Display(Name = "物品编码")]
        public int InventoryId { get; set; }

        [Display(Name = "领用人")]
        public string ApplicantName { get; set; }

        [Display(Name = "领用日期")]
        public DateTime ApplicationDate { get; set; }

        [Display(Name = "数量")]
        public int Quantity { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }
    }
}
