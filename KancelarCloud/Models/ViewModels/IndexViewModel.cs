using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KancelarCloud.Models;

namespace KancelarCloud.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<FileVloj> FileVlojs { get; set; }
        public IEnumerable<Org> Orgs { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
