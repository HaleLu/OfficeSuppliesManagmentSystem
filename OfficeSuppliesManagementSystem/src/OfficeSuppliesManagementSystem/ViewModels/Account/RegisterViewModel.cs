﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSuppliesManagementSystem.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次输入不一致！")]
        public string ConfirmPassword { get; set; }
    }
}
