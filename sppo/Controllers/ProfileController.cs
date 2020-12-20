﻿using System;
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

                };//provjera
                if (model.ProfileImage != null)
                {
                    if (!model.ProfileImage.ContentType.Contains("image"))
                    {
                        ModelState.AddModelError(nameof(model.ProfileImage), "You can only upload image.");
                        //return View(model);
                    }
                    else
                    {
                        a.ProfilePicture = uniquFileName;
                    }
                }
                else
                {
                    a.ProfilePicture = null;
                }
                _db.Add(a);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditPicture(string profileID)
        {
            Profile profile = _db.profiles.Find(profileID);

            if (profile == null)
                return View();

            ProfileEditPictureVM obj = new ProfileEditPictureVM
            {
                ID = profile.Id,
                ExistingProfileImagePath = profile.ProfilePicture,


            };

            return View(obj);
        }

        [HttpPost]
        public IActionResult EditPicture(ProfileEditPictureVM obj)
        {
            Profile profile = _db.profiles.Find(obj.ID);
            if (profile != null)
            {
                if (obj.ExistingProfileImagePath != null)
                {
                    if (!obj.ProfileImage.ContentType.Contains("image"))
                    {
                        ModelState.AddModelError(nameof(obj.ProfileImage), "You can only upload image.");
                        return View(obj);
                    }
                    else
                    {
                        profile.ProfilePicture = UploadedPicture(obj);
                    }
                }

                _db.Update(profile);
                _db.SaveChanges();
                return Redirect("/Profile/Details/" + obj.ID);
            }
            return View("Create");

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
