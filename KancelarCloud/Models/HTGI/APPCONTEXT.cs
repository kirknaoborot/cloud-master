using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KancelarCloud.Models.HTGI;

namespace KancelarCloud.Models.HTGI
{
    public class APPCONTEXT :DbContext
    {
        public DbSet<DOCUMENT> DOCUMENTS { get; set; }
        public DbSet<IMAGE> IMAGES { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {    
                optionsBuilder.UseSqlServer(@"Server=;Database=;User Id=; Password=;");
        }
    }
}
