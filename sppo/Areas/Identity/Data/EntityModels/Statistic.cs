using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Statistic
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int? LogId { get; set; }
        public Log Log { get; set; }

    }
}
