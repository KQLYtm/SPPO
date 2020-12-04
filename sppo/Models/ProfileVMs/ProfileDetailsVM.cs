using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.ProfileVMs
{
    public class ProfileDetailsVM
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
        public DateTime? CreateDate { get; set; }
        public float? AvgGrade { get; set; }
        public string User { get; set; }
        public string Company { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
    }
}
