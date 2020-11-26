using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public DateTime ExpDate { get; set; }
        public string SecurityCode { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }

    }
}
