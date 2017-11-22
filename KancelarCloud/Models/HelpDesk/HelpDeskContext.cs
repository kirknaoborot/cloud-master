using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KancelarCloud.Models.HelpDesk;

namespace KancelarCloud.Models.HelpDesk
{
    public class HelpDeskContext: DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
                builder.UseSqlServer(@"Server=10.0.0.8;Database=HelpDesk;User Id=sa; Password=Qwer1234; MultipleActiveResultSets=true;");
            
          
        }
    }
}
