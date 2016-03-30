using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using OfficeSuppliesManagementSystem.Utilities;

namespace OfficeSuppliesManagementSystem.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
    }
    
    public class ApplicationUser : IdentityUser<int>
    {
        public enum EGnder
        {
            [Display(Name = "")]
            Unset = 0x0,

            [Display(Name = "男")]
            Male = 0x1,

            [Display(Name = "女")]
            Female = 0x2
        }
        
        [Display(Name = "用户ID")]
        public override string UserName { get; set; }
        
        [Display(Name = "联系电话")]
        public override string PhoneNumber { get; set; }
        
        [MaxLength(256)]
        [Display(Name = "真实姓名")]
        public string RealName { get; set; }

        [Display(Name = "性别")]
        public EGnder Gender { get; set; }

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
