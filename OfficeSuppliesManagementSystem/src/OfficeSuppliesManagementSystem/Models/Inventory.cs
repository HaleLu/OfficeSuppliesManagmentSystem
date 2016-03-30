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

        public int Quantity { get; set; }

        [Required]
        [ForeignKey("SupplyId")]
        public Supply Supply { get; set; }
        
        public IEnumerable<ImportRecord> ImportRecords { get; set; }
        
        public IEnumerable<ExportRecord> ExportRecords { get; set; }
    }
}
