using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models
{
    public class NotificationsVM
    {
        public class Row
        {
            public int NotId { get; set; }
            public string From { get; set; }
            public string What { get; set; }
            public string Detail { get; set; }
            public bool IsRead { get; set; }
            public string Date { get; set; }
        }
        public List<Row> rows { get; set; }
    }
}
