using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class LoyaltyPoints
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }

    }
}
