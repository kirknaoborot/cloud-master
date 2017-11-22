﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KancelarCloud.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Login")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }
        [Display(Name ="Запомнить?")]
        public bool RememberMe { get; set; }
        
    }
}