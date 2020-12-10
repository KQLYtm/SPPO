using Microsoft.AspNetCore.Http;
using SPPO.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Models
{
    public class FormVM
    {

            public int Id { get; set; }
            public int? Experience { get; set; }
            public bool? Employed { get; set; }
            public string Cv { get; set; }
            public string MotivationMessage { get; set; }
            public bool? DrivingLicence { get; set; }
            public int? NumberOfLanguages { get; set; }
            public string User { get; set; }
            public string Company { get; set; }
            public string Advertisement { get; set; }



    }
}
