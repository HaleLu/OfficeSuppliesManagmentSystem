using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSuppliesManagementSystem.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Display(Name = "库存数量")]
        public int Quantity { get; set; }

        [Required]
        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; }
        
        public ICollection<ImportRecord> ImportRecords { get; set; }
        
        public ICollection<ExportRecord> ExportRecords { get; set; }
    }
}
