using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSuppliesManagementSystem.Models
{
    public class ImportRecord
    {
        [Display(Name = "入库流水号")]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        [Display(Name = "购买日期")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "购买数量")]
        public int Quantity { get; set; }

        [Display(Name = "单价")]
        public double UnitPrice { get; set; }

        [Display(Name = "总价 ")]
        public double TotalPrice { get; set; }

        [Required]
        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }
    }
}
