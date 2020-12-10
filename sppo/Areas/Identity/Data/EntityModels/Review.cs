using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Commentary { get; set; }
        public string GiverId { get; set; }
        public Profile Giver { get; set; }
        public string ReciverId { get; set; }
        public Profile Reciver { get; set; }
    }
}
