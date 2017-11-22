using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KancelarCloud.Models.HelpDesk
{
    public class AspNetUser
    {
        [Key]
        public string Id { get; set; }
        //public string UserName { get; set; }

    }
}
