using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models
{
    public class UserRolesVM
    {
        public string userId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public IEnumerable<string> Roles { get; set; }

    }
}
