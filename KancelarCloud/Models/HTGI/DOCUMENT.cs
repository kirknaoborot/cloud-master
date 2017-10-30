using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KancelarCloud.Models.HTGI
{
    public class DOCUMENT
    {
        [Key]
        public Guid UID { get; set; }
        //public int DOCTYPE_ID { get; set; }
        //public string REG_NUM { get; set; }
        //public string DOC_NUM { get; set; }
        //public DateTime IZD_DATE { get; set; }
        //public DateTime REG_DATE { get; set; }
        //public DateTime SAVE_DATE { get; set; }
        //public DateTime CORR_DATE { get; set; }
        //public string AUTHOR_UID { get; set; }
        //public string OPERATOR_UID { get; set; }
        //public int KONTROL_ID { get; set; }
        public string NAME { get; set; }
        public string REMARK { get; set; }
        //public int PRUZ { get; set; }
        //public string BARCODE { get; set; }
        //public int LOCKED { get; set; }
        //public int NUMSEARCH { get; set; }


    }
}
