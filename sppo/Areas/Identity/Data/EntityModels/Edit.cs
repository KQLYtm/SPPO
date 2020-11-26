using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Edit
    {
        public int Id { get; set; }
        public string EditUserName { get; set; }
        public string EditPassword { get; set; }
        public byte[] EditImage { get; set; }
        public string EditPasswordHash { get; set; }
        public DateTime EditChangeTime { get; set; }
        public int AccountId { get; set; }
        public Profile Account { get; set; }

    }
}
