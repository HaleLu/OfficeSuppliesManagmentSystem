using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSuppliesManagementSystem.ViewModels.ApplicationUser
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "用户ID")]
        public string UserName { get; set; }

        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }

        [MaxLength(256)]
        [Display(Name = "真实姓名")]
        public string RealName { get; set; }

        [Display(Name = "性别")]
        public Models.ApplicationUser.EGnder Gender { get; set; }

        [Display(Name = "出生日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime? Birthday { get; set; }

        [MaxLength(128)]
        [Display(Name = "籍贯")]
        public string NativePlace { get; set; }

        [MaxLength(4096)]
        [Display(Name = "简介")]
        public string About { get; set; }
    }
}
