using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.Forms
{
    public class ApplayToJobVM
    {
        public string ProfileId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Cv { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string CompanyName { get; set; }
        public int WorkExpirience { get; set; }
        public string Note { get; set; }
        public int AdvertisementId { get; set; }
    }
}
