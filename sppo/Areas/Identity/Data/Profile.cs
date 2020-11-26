using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SPPO.EntityModels;

namespace sppo.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Profile class
    public class Profile : IdentityUser
    {
        public byte[] Image { get; set; }
        public DateTime? CreateDate { get; set; }
        public float? AvgGrade { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public int? CompanyID { get; set; }
        public Company Company { get; set; }
        public int? LanguageId { get; set; }
        public Language Language { get; set; }
        public int? ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}
