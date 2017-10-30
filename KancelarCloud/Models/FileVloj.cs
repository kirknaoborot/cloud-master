using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KancelarCloud.Models
{
    public class FileVloj
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string PathFileName { get; set; }
        public DateTime EnterDate { get; set; }
        public string Type { get; set; }
        public int SizeFile { get; set; }
        public bool DeleteFile { get; set; }
       
      
      
    }
}
