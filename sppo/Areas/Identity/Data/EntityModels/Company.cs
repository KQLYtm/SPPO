using Microsoft.EntityFrameworkCore.Migrations;
using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[]? Image { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyRepresenterFirstName { get; set; }
        public string CompanyRepresenterLastName { get; set; }
        public string Adress { get; set; }
        public Profile Account { get; set; }
        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public int? LoyaltyPointsId { get; set; }
        public LoyaltyPoints LoyaltyPoints { get; set; }


    }
}
