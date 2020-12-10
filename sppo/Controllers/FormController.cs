using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sppo.Data;
using sppo.Models;
using SPPO.EntityModels;

namespace sppo.Controllers
{
    public class FormController : Controller
    {
        private readonly MyContext _db;
        private IHostingEnvironment _env;

        public FormController(MyContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {

            var form = await _db.forms.Select(a => new FormVM
            {
               Id=a.Id,
               Experience=a.Experience,
               Employed=a.Employed,
               Cv=a.Cv,
               MotivationMessage=a.MotivationMessage,
               DrivingLicence=a.DrivingLicence,
               NumberOfLanguages=a.NumberOfLanguages

            }).ToListAsync();

            return View(form);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file,FormVM formVM)
        {
            var Cv = System.IO.Path.GetFileName(file.FileName);
            var CvPath = System.IO.Path.Combine(_env.WebRootPath, "file", Cv);

            //check if its pdf format!
            string pdf = Path.GetExtension(Cv);
            if (pdf.ToLower() != ".pdf")
            {
                return View();
            }
            if (file.Length > 0)
            {
                using (var stream = new FileStream(CvPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                formVM.Cv = Cv;
            }
            var form = new Form
            {
                Cv = formVM.Cv,
            };
            _db.forms.Add(form);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult GetCv(string CvName)
        {
            string filePath = "~/file/" + CvName;
            Response.Headers.Add("Content-Disposition", "inline; filename=" + CvName);
            return File(filePath, "application/pdf");
        }
    }
}
