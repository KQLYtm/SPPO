using Microsoft.AspNetCore.Http;
using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] Cv { get; set; }
        public string HighSchoolName { get; set; }
        public string CollegeName { get; set; }
        public string Address { get; set; }
        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public int? LoyaltyPointsId { get; set; }
        public LoyaltyPoints LoyaltyPoints { get; set; }
        public Profile Account { get; set; }

    }
}
