using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.ReviewVM
{
    public class ReviewsVM
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        public string Commentary { get; set; }
        public string GiverUserName { get; set; }
        public string GiverPicture { get; set; }
    }
}

