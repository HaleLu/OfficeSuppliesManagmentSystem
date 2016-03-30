﻿using System;
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
        public int Id { get; set; }

        public string ApplicantName { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }

        public EApplyStatus Status { get; set; }

        [ForeignKey("InventoryId")]
        public Inventory Inventory { get; set; }
    }
}
