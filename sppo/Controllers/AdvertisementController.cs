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
                Name = a.Name,
                CurrentUserId = _userManager.GetUserId(User)

            }).SingleOrDefault();

            return View(ad);

        }
        public IActionResult GetAll()
        {
            List<AdvertisementAddVM> model = _context.advertisements.Select(a => new AdvertisementAddVM
            {
                Id = a.Id,
                CompanyName = a.Profile.Company.Name,
                UserName = a.Profile.User.FirstName + ' ' + a.Profile.User.LastName,
                JobId = a.JobId,
                Description = a.Description,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                ProfileId = a.ProfileId,
                Job = a.Job.Name,
                Name = a.Name,
                ProfileImg = a.Profile.ProfilePicture,
                CurrentUserId = _userManager.GetUserId(User)


            }).ToList();
            return View(model);
        }
        [Authorize]
        public IActionResult Add(int AdvId)
        {
            AdvertisementAddVM aavm;
            List<SelectListItem> jobs = _context.jobs.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            if (AdvId == 0)
            {
                aavm = new AdvertisementAddVM();
                aavm.jobs = jobs;
            }
            else
            {
                aavm = _context.advertisements.Where(a => a.Id == AdvId).Select(a => new AdvertisementAddVM
                {
                    Id = a.Id,
                    Name = a.Name,
                    JobId = a.JobId,
                    jobs = jobs,
                    CurrentUserId = _userManager.GetUserId(User),
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    ProfileImg = a.Profile.ProfilePicture,
                    Description=a.Description,




                }).FirstOrDefault();


            }
            return View("Add", aavm);

        }
        public IActionResult Save(AdvertisementAddVM m)
        {
            if (ModelState.IsValid == true)
            {
                Advertisement a;
                if (m.Id == 0)
                {
                    a = new Advertisement();
                    a.StartDate = DateTime.Now;
                    _context.Add(a);
                }
                else
                {
                    a = _context.advertisements.Find(m.Id);
                }
                a.Name = m.Name;
                a.Description = m.Description;
                a.EndDate = m.EndDate;
                a.JobId = m.JobId;
                a.StartDate = DateTime.Now;
                a.ProfileId = _userManager.GetUserId(User);
                _context.SaveChanges();
            }
            return Redirect("/Home/Index");
        }

    }
}

