using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sppo.Areas.Identity.Data;
using sppo.Data;
using sppo.Models;
using sppo.Models.ProfileVMs;

namespace sppo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<Profile> _userManager;
        private readonly MyContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(MyContext db, IWebHostEnvironment webHostEnvironment,
            UserManager<Profile> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


            var profile = _db.profiles.Select(a => new ProfileVM
            {
                Id = a.Id,
                Username = a.UserName,
                Email = a.Email,
                ProfilePicture = a.ProfilePicture,
                User = a.User.Account.UserName,
                Company =a.Company.Account.UserName,
                Language=a.Language.Name,
                Theme=a.Theme.Name

            }).ToList();


            return View(profile);
        }
       

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ProfileDetailsVM model)
        {
            if (ModelState.IsValid)
            {
                string uniquFileName = UploadedFile(model);
                Profile a = new Profile
                {
                   UserName=model.Username,
                   Email=model.Email,
                   ProfilePicture=uniquFileName

                };

                _db.Add(a);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ProfileDetailsVM model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
