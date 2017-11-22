using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KancelarCloud.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Ф.И.О.")]
        public string FIO { get; set; }
        [Required]
        [Display(Name ="Логин")]
        public string Login { get; set; }
        [Required]
        [Display(Name ="Пароль")]
        [DataType(DataType.Password)]
        [StringLength(100,ErrorMessage ="Длинна пароля должна составлять не менее 5 символов",MinimumLength =5)]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Пароли не совпадают")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

    }
}
