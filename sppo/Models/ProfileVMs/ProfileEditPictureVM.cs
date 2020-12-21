using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models.ProfileVMs
{
    public class ProfileEditPictureVM:ProfileDetailsVM
    {
        public string ID { get; set; }
        public string ExistingProfileImagePath { get; set; }
    }
}
