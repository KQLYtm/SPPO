using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class LogUserVisited
    {
        public bool IsVisited { get; set; }
        public int? LogId { get; set; }
        public Log Log { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }


    }
}
