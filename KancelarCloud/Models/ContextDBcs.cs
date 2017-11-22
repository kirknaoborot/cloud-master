using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KancelarCloud.Models
{
    public class ContextDBcs :DbContext
    {
        public DbSet<FileVloj> FileVlojs { get; set; }
        public DbSet<Org> Orgs { get; set; }



        public ContextDBcs(DbContextOptions<ContextDBcs> options)
            : base(options)
        {

        }


    }
}
