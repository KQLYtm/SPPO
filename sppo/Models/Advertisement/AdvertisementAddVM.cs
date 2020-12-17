using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.Advertisement
{
    public class AdvertisementAddVM:IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ProfileImg { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? JobId { get; set; }
        public string Job { get; set; }
        public string ProfileId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string CurrentUserId { get; set; }


        public List<SelectListItem> jobs { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    errorMessage: "EndDate must be greater than StartDate",
                    memberNames: new[] { "EndDate" }
               );
            }
        }
       
    }
}
