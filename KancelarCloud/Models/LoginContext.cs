using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KancelarCloud.Models
{
    public class LoginContext:IdentityDbContext<User>
    {
        public LoginContext(DbContextOptions<LoginContext>options):base(options)
        {

        }
    }
}
