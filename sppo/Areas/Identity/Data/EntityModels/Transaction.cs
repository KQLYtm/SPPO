using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public int CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }

    }
}
