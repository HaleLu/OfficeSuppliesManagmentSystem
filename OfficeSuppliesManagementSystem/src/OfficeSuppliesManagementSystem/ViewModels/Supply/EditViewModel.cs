using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace OfficeSuppliesManagementSystem.ViewModels.Supply
{
    public class EditViewModel
    {
        public int Id { set; get; }

        [Display(Name = "物品名称")]
        public string Name { set; get; }

        [Display(Name = "物品类别")]
        public Models.Supply.ESupplyType Type { set; get; }

        [Display(Name = "产地")]
        public Models.Supply.EProvince Province { set; get; }

        [Display(Name = "规格")]
        public string Specification { set; get; }

        [Display(Name = "型号")]
        public string Serial { set; get; }
    }
}
