using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSuppliesManagementSystem.ViewModels.ImportRecord
{
    public class CreateViewModel
    {
        [Display(Name = "物品编码")]
        public int InventoryId { get; set; }

        [Display(Name = "购买日期")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "购买数量")]
        public int Quantity { get; set; }

        [Display(Name = "总价")]
        public double TotalPrice { get; set; }
    }
}
