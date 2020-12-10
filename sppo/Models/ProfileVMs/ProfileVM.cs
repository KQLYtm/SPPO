using Microsoft.AspNetCore.Http;
using sppo.Areas.Identity.Data;
using sppo.Models.ReviewVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models
{
    public class ProfileVM
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Profile Picture")]
        public string ProfilePicture{ get; set; }
        public DateTime? CreateDate { get; set; }
        public float? AvgGrade { get; set; }
        public string AdressUser { get; set; }
        public string AdressCompany { get; set; }
        public string PhoneNumber { get; set; }
        public string User { get; set; }
        public string Company { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
        public float? Grade { get; set; }
        public bool MyDetails { get; set; }

        public List<ReviewsVM> reviews { get; set; }

    }
}
