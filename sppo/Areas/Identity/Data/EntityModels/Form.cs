using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Form
    {
        public int Id { get; set; }
        public int? Experience { get; set; }
        public string Cv { get; set; }
        public string MotivationMessage { get; set; }
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
