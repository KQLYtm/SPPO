using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using sppo.Areas.Identity.Data;
using sppo.Data;
using sppo.Models.Advertisement;
using SPPO.EntityModels;

namespace sppo.Controllers
{

    public class AdvertisementController : Controller
    {
        private readonly MyContext _context;
        private readonly UserManager<Profile> _userManager;
        public AdvertisementController(MyContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Details(int AdvId)
        {
            AdvertisementAddVM ad = _context.advertisements.Where(a => a.Id == AdvId).Select(a => new AdvertisementAddVM
            {
                Id = a.Id,
                Job = a.Job.Name,
                ProfileImg = a.Profile.ProfilePicture,
                Description = a.Description,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                ProfileId = a.ProfileId,
                CompanyName = a.Profile.Company.Name,
                UserName = a.Profile.User.FirstName + ' ' + a.Profile.User.LastName,
                Name=a.Name

            }).SingleOrDefault();

            return View(ad);
        
        }
            public IActionResult GetAll()
        {
            List<AdvertisementAddVM> model = _context.advertisements.Select(a => new AdvertisementAddVM
            {
                Id = a.Id,
                CompanyName=a.Profile.Company.Name,
                UserName=a.Profile.User.FirstName + ' ' +a.Profile.User.LastName,
                JobId = a.JobId,
                Description = a.Description,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                ProfileId = a.ProfileId,
                Job = a.Job.Name,
                Name=a.Name,
                ProfileImg=a.Profile.ProfilePicture

            }).ToList();
            return View(model);
        }
        [Authorize]
        public IActionResult Add()
        {
            List<SelectListItem> jobs = _context.jobs.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            AdvertisementAddVM aavm = new AdvertisementAddVM();
            aavm.jobs = jobs;

            return View(aavm);
        }
        public IActionResult Save(AdvertisementAddVM m)
        {
            if (ModelState.IsValid == true)
            {
                Advertisement a = new Advertisement();
                a.Name = m.Name;
                a.Description = m.Description;
                a.StartDate = DateTime.Now;
                a.EndDate = m.EndDate;
                a.JobId = m.JobId;
                a.ProfileId = _userManager.GetUserId(User);
                _context.Add(a);
                _context.SaveChanges();
            }
            return Redirect("/Home/Index");
        }
    }
}

