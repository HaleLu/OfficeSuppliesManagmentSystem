using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSuppliesManagementSystem.Models
{
    public class ExportRecord
    {
        public enum EApplyStatus
        {
            [Display(Name = "待处理")]
            Pending = 0x0,
            [Display(Name = "已批准")]
            Accepted = 0x1,
            [Display(Name = "已拒绝")]
            Rejected = 0x2
        }

        [Display(Name = "流水号")]
        public int Id { get; set; }

        [Display(Name = "领用人")]
        public string ApplicantName { get; set; }

        [Display(Name = "领用日期")]
        public DateTime ApplicationDate { get; set; }

        [Display(Name = "数量")]
        public int Quantity { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

        [Display(Name = "申请状态")]
        public EApplyStatus Status { get; set; }

        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }
    }
}
