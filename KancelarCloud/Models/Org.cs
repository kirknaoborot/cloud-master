using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KancelarCloud.Models
{
    public class Org
    {
        
        public int OrgId { get; set; }
        [Required (ErrorMessage = "Не указано название организации")]
        public string NameOrg { get; set; }
        [Required (ErrorMessage ="Не указан адрес электронной почты")]
        [RegularExpression (@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
       
      
    }
}
