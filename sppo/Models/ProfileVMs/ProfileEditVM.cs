using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPPO.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.ProfileVMs
{
    public class ProfileEditVM:ProfileDetailsVM
    {
        public string ProfileId { get; set; }
        public string ExistingProfileImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string HighSchoolName { get; set; }
        public string CollegeName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender{ get; set; }
        public string ProfilePicture { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string CityName { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<SelectListItem> cities{ get; set; }
        public List<SelectListItem> countries { get; set; }
        public List<SelectListItem> genders { get; set; }

    }
}
