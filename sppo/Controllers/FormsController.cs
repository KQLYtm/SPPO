using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sppo.Areas.Identity.Data;
using sppo.Data;
using sppo.Models.Forms;
using SPPO.EntityModels;

namespace sppo.Controllers
{
    public class FormsController : Controller
    {
        private MyContext _context;
        private IHostingEnvironment _env;
        private UserManager<Profile> _userManager;
        public FormsController(MyContext context, IHostingEnvironment env, UserManager<Profile> user)
        {
            _context = context;
            _env = env;
            _userManager = user;
        }

        public IActionResult ViewAll(int id)
        {
            var model = new FormViewVM
            {
                Id = id,
                rows = _context.forms.Where(s => s.AdvertisementId==id).Select(a => new FormViewVM.Row
                {
                    ProfileId=a.ProfileId,
                    Profile=a.Profile.UserName,
                    AdvertisementId=a.AdvertisementId,
                    Advertisement=a.Advertisement.Name,
                    Experience=a.Experience,
                    MotivationMessage=a.MotivationMessage
                }).ToList()
            };
            return View(model);
        }
        public IActionResult SubmitApply(int AdvId,ApplayToJobVM vm)
        {
            //2 metod
            //MemoryStream ms = new MemoryStream();
            //var f = vm.Cv.OpenReadStream();

            //f.CopyTo(ms);
            //byte[] c1 = ms.ToArray();
            Form form = new Form
            {
                Experience = vm.WorkExpirience,
                MotivationMessage = vm.Note,
                ProfileId = _userManager.GetUserId(User),
                Profile = _context.profiles.Find(_userManager.GetUserId(User)),
                AdvertisementId = AdvId,
                Advertisement = _context.advertisements.Find(vm.AdvertisementId),
                Cv = GetByteArrayFromCv(vm.Cv)
            };
          
            _context.Add(form);
            _context.SaveChanges();
            return RedirectToAction("GetAll", "Advertisement");
        }
        public IActionResult ApplayToJob(string ProfileId,int AdvId)
        {
            Profile profile = _context.profiles.Where(a => a.Id == ProfileId).FirstOrDefault();

            var userId = profile.UserID != null ? profile.UserID : null;
            var companyId = profile.CompanyID != null ? profile.CompanyID : null;

            var user = userId != null ? _context.users.Find(userId) : null;
            var company = companyId != null ? _context.companies.Find(companyId) : null;


            ApplayToJobVM app = new ApplayToJobVM();

            app.ProfileId = profile.Id;
            app.Email = profile.Email;
            app.PhoneNumber = profile.PhoneNumber;
            app.CompanyName = company != null ? company.Name : null;
            app.City = company != null ?_context.cities.Find(company.CityId).Name : _context.cities.Find(user.CityId).Name;
            app.Address = company != null ? company.Adress : user.Address;
            app.AdvertisementId = AdvId;
            

            if (user != null) {
                app.FirstName = user.FirstName;
                app.LastName = user.LastName;
                app.BirthDate = user.BirthDate;
                app.Gender = user.Gender != null ? user.Gender.Name : null;
            }

            return View(app);
        }
        private byte[] GetByteArrayFromCv(IFormFile file)
        {
            using(var target=new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }
    }
}
