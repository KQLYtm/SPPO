using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class AccountGrade
    {
        public bool? Graded { get; set; }
        public float? Grade { get; set; }
        public string GiversID { get; set; }
        [ForeignKey("GiversID")]
        public Profile Givers { get; set; }
        public string RecieverID { get; set; }
        [ForeignKey("RecieverID")]
        public Profile Reciever { get; set; }
    }
}
