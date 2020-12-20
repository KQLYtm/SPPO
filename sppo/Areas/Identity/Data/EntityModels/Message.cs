using sppo.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPO.EntityModels
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime SentDate { get; set; }
        public string Content { get; set; }
        public string GiverId { get; set; }
        public Profile Giver { get; set; }
        public string Receiverd { get; set; }
        public Profile Receiver { get; set; }
        public int? AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
