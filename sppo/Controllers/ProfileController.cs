using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var user = _db.profiles.Where(x => x.Id == profileID).FirstOrDefault();
            User u = _db.users.Where(x => x.Id == user.UserID).FirstOrDefault();
            Company c = _db.companies.Where(x => x.Id == user.CompanyID).FirstOrDefault();

            var profile = await _db.profiles.Where(s => s.Id == profileID).Select(a => new ProfileVM
            {
                Id = a.Id,
                Email = a.Email,
                ProfilePicture = a.ProfilePicture,
                PhoneNumber = a.PhoneNumber

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
            Profile p = _db.profiles.Where(x => x.Id == id).FirstOrDefault();
            User u = _db.users.Where(x => x.Id == p.UserID).FirstOrDefault();
            Company c = _db.companies.Where(x => x.Id == p.CompanyID).FirstOrDefault();

            // added comment
            vm.Id = profile.Id;
            vm.FirstName = u != null ? u.FirstName : c.CompanyRepresenterFirstName;
            vm.LastName = u != null ? u.LastName : c.CompanyRepresenterLastName;
            vm.Company = c != null ? c.Name : null;
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
                FirstName = a.UserName,
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

        private Country ReturnCountry(int? CityId)
        {
            Country c = new Country();
            var city = _db.cities.Where(x => x.Id == CityId).FirstOrDefault();
            var country = _db.countries.Where(x => x.Id == city.CountryId).FirstOrDefault();
            c.Name = country.Name;
            return c;
        }

        public IActionResult EditProfile(string ProfileId)
        {
            Profile p = _db.profiles.Find(ProfileId);
            ProfileEditVM pe = new ProfileEditVM();
            if (p.UserID != null)
            {
                User u = _db.users.Where(x => x.Id == p.UserID).FirstOrDefault();
                pe.Country = ReturnCountry(u.CityId);
                pe.CountryId = u.City.CountryId;
                pe.FirstName = u.FirstName;
                pe.LastName = u.LastName;
                pe.HighSchoolName = u.HighSchoolName;
                pe.Adress = u.Address;
                pe.CollegeName = u.CollegeName;
                pe.GenderId = u.GenderId;
                pe.CityName = u.City.Name;
                pe.BirthDate = u.BirthDate;
            }
            else
            {
                Company c = _db.companies.Where(x => x.Id == p.CompanyID).FirstOrDefault();
                pe.FirstName = c.CompanyRepresenterFirstName;
                pe.LastName = c.CompanyRepresenterLastName;
                pe.Country = ReturnCountry(c.CityId);
                pe.CountryId = c.City.CountryId;
                pe.Adress = c.Adress;
                pe.CompanyName = c.Name;
                pe.CityName = c.City.Name;
            }

            pe.ProfileId = p.Id;
            pe.Email = p.Email;
            pe.ProfilePicture = p.ProfilePicture;
            pe.PhoneNumber = p.PhoneNumber;

            List<SelectListItem> cities = _db.cities.Where(s => s.CountryId == pe.CountryId).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            List<SelectListItem> countries = _db.countries.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            List<SelectListItem> genders = _db.genders.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            pe.cities = cities;
            pe.countries = countries;
            pe.genders = genders;

            return View(pe);
        }

        public List<SelectListItem> RefreshCities(int CountryId)
        {
            List<SelectListItem> cities = _db.cities.Where(s => s.CountryId == CountryId).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToList();
            return cities;
        }

        public IActionResult SaveChanges(ProfileEditVM vm)
        {
            Profile p = _db.profiles.Find(vm.ProfileId);
            User user = _db.users.Where(x => x.Id == p.UserID).FirstOrDefault();
            Company company = _db.companies.Where(s => s.Id == p.CompanyID).FirstOrDefault();
            if (p != null && user != null)
            {
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.Address = vm.Adress;
                user.CollegeName = vm.CollegeName;
                user.BirthDate = vm.BirthDate;
                user.PhoneNumber = vm.PhoneNumber;
                user.CityId = vm.CityId;
                user.HighSchoolName = vm.HighSchoolName;
                user.GenderId = vm.GenderId;
            }
            else if (p != null && company != null)
            {
                company.CompanyRepresenterFirstName = vm.FirstName;
                company.CompanyRepresenterLastName = vm.LastName;
                company.Adress = vm.Adress;
                //company.PhoneNumber = vm.PhoneNumber;
                company.CityId = vm.CityId;
                if (vm.CompanyName != null)
                    company.Name = vm.CompanyName;
                else
                    company.Name = "Company name can not be empty. You are registered as company. Set your company name!";
            }

            _db.SaveChanges();
            return Redirect("/Profile/Details?id=" + vm.ProfileId);
        }

        public IActionResult EditPicture(ProfileEditVM obj)
        {
            Profile profile = _db.profiles.Find(obj.ProfileId);
            ProfileEditVM edit = new ProfileEditVM
            {
                ProfileId = profile.Id,
                ExistingProfileImagePath = profile.ProfilePicture
            };
            if (profile != null)
            {
                if (edit.ExistingProfileImagePath != null)
                {
                    if (!obj.ProfileImage.ContentType.Contains("image"))
                    {
                        ModelState.AddModelError("", "You can only upload image.");
                    }
                    else
                        profile.ProfilePicture = UploadedPicture(obj);
                }
                _db.Update(profile);
                _db.SaveChanges();
            }
            return Redirect("/Profile/EditProfile?ProfileId=" + profile.Id);
        }
    }
}