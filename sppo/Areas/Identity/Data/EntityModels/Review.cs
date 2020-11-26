using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Commentary { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

    }
}
