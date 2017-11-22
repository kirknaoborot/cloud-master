using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KancelarCloud.Models.HelpDesk
{
    public class Ticket
    {
        //public string Email { get; set; }
        //public string FIO { get; set; }
        //public string Phone { get; set; }
        [Key]
        public string UsersId { get; set; }
        //public int DepartsmentId { get; set; }
        //public int TopicsId { get; set; }
        //public string Message { get; set; }
        //public DateTime Date { get; set; }
        //public int Close { get; set; }
        //public DateTime CloseDate { get; set; }
        //public int PriorityId { get; set; }
        //public Guid CloseId { get; set; }
        //public Guid CreateId { get; set; }
        //public DateTime LateDate { get; set; }
    }
}
