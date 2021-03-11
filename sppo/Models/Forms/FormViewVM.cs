using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.Forms
{
    public class FormViewVM
    {
        public int Id { get; set; }
        public class Row
        {
            public int? Experience { get; set; }
            //public string Cv { get; set; }
            public string MotivationMessage { get; set; }
            public string ProfileId { get; set; }
            public string Profile { get; set; }
            public int AdvertisementId { get; set; }
            public string Advertisement { get; set; }
        }
        public List<Row> rows { get; set; }
    }
}
