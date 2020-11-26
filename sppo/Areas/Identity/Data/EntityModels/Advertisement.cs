using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace SPPO.EntityModels
{
    public class Advertisement
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? JobId { get; set; }
        public Job Job { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public Log Log { get; set; }

    }
}
