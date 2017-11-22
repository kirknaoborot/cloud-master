using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KancelarCloud.Models
{
    public class User:IdentityUser
    {
        public string FIO { get; set; }
    }
}
