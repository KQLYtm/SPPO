using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Notification
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        //public string NotiHeader { get; set; }
        public string NotiBody { get; set; }
        public bool IsRead { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
        public string FromUserName { get; set; }

        //public string CreatedDateSt => this.CreatedDate.ToString("dd-MMM-yyyy HH:mm:ss");
        //public string IsReadSt => this.IsRead ? "YES" : "NO";


    }
}
