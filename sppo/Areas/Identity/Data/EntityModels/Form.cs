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
        public bool? Employed { get; set; }
        public byte[] Cv { get; set; }
        public string MotivationMessage { get; set; }
        public bool? DrivingLicence { get; set; }
        public int? NumberOfLanguages { get; set; }
        public string ProfileId { get; set; }
        public User Profile { get; set; }
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
