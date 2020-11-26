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
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
