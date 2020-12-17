using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sppo.Areas.Identity.Data;
using sppo.Data;
using sppo.Models;
using sppo.Models.ProfileVMs;
using sppo.Models.ReviewVM;
using SPPO.EntityModels;

namespace sppo.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<Profile> _userManager;
        private readonly SignInManager<Profile> _signInManager;
        private readonly MyContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(MyContext db, IWebHostEnvironment webHostEnvironment,
            UserManager<Profile> userManager, SignInManager<Profile> signInManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Show(string profileID)
        {
            var profile = await _db.profiles.Where(s => s.Id == profileID).Select(a => new ProfileVM
            {
                Id = a.Id,
                Username = a.UserName,
                Email = a.Email,
                ProfilePicture = a.ProfilePicture,


            }).ToListAsync();

            return View(profile);
        }
        public async Task<IActionResult> Details(string id)
        {
            var ag = _db.accountGrades.Where(a => a.RecieverID == id && a.GiversID == _userManager.GetUserId(User)).FirstOrDefault();
            Profile profile;
            ProfileVM vm = new ProfileVM();
            if (id != _userManager.GetUserId(User))
            {
                var Lista = _db.reviews.Where(a => a.ReciverId == id).Select(a => new ReviewsVM
                {
                    Id = a.Id,
                    GiverUserName = a.Giver.UserName,
                    PostDate = a.PostDate,
                    Commentary = a.Commentary,
                    GiverPicture = a.Giver.ProfilePicture

                }).ToList();

                profile = _db.profiles.Where(a => a.Id == id).FirstOrDefault();
                vm.MyDetails = false;
                vm.reviews = Lista;

                if (ag != null)
                    vm.Grade = ag.Grade;
                else
                    vm.Grade = 0;

            }
            else
            {
                var user = _userManager.GetUserId(User);
                profile = _db.profiles.Where(x => x.Id == user).FirstOrDefault();
                vm.MyDetails = true;
                vm.Grade = -1;

                var Lista = _db.reviews.Where(a => a.ReciverId == user).Select(a => new ReviewsVM
                {
                    Id = a.Id,
                    GiverUserName = a.Giver.UserName,
                    PostDate = a.PostDate,
                    Commentary = a.Commentary,
                    GiverPicture = a.Giver.ProfilePicture

                }).ToList();

                vm.reviews = Lista;

            }


            vm.Id = profile.Id;
            vm.Username = profile.UserName;
            vm.Email = profile.Email;
            vm.CreateDate = profile.CreateDate;
            vm.AvgGrade = profile.AvgGrade;
            vm.PhoneNumber = profile.PhoneNumber;
            vm.ProfilePicture = profile.ProfilePicture;
            vm.User = profile.User?.FirstName;
            vm.AdressUser = profile.User?.Address;
            vm.AdressCompany = profile.Company?.Adress;
            vm.Company = profile.Company?.Name;
            vm.Language = profile.Language?.Name;
            vm.Theme = profile.Theme?.Name;
            return View(vm);

        }
        public async Task<IActionResult> Index()
        {

            var profile = await _db.profiles.Select(a => new ProfileVM
            {
                Id = a.Id,
                Username = a.UserName,
                Email = a.Email,
                ProfilePicture = a.ProfilePicture,
                //User = a.User.Account.UserName,
                //Company =a.Company.Account.UserName,
                //Language=a.Language.Name,
                //Theme=a.Theme.Name

            }).ToListAsync();


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
                string uniquFileName = UploadedPicture(model);
                Profile a = new Profile
                {
                    UserName = model.Username,
                    Email = model.Email,
                    ProfilePicture = uniquFileName

                };

                _db.Add(a);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProfile(string ProfileId)
        {
            var p = await _userManager.FindByIdAsync(ProfileId);
            User u = _db.users.Where(x => x.Id == p.UserID).SingleOrDefault();
            if (u != null)
            {
                _db.Remove(u);
            }
            else if (u == null && p != null)
            {
                Company c = _db.companies.Where(x => x.Id == p.CompanyID).SingleOrDefault();
                _db.Remove(c);
            }

            var adv = _db.advertisements.Where(x => x.ProfileId == p.Id).FirstOrDefault();
            var reviews = _db.reviews.Where(x => x.ReciverId == p.Id || x.GiverId == p.Id).ToList();
            var accoundgrades = _db.accountGrades.Where(x => x.RecieverID == p.Id).ToList();

            if (adv != null)
                _db.Remove(adv);
            if (reviews != null)
            {
                foreach (var x in reviews)
                {
                    _db.Remove(x);
                }
            }
            if (accoundgrades != null)
            {
                foreach (var x in accoundgrades)
                {
                    _db.Remove(x);
                }
            }
            _db.Remove(p);
            _db.SaveChanges();
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

        private string UploadedPicture(ProfileDetailsVM model)
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
