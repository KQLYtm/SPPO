using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sppo.Areas.Identity.Data;
using sppo.Data;
using sppo.IService;
using sppo.Models;
using SPPO.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly MyContext _db;
        private readonly UserManager<Profile> _userManager;
        INotiService _nootiService = null;
        List<Notification> _oNotifications = new List<Notification>();

        public NotificationsController(INotiService notiService, MyContext db, UserManager<Profile> userManager)
        {
            _nootiService = notiService;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult AllNotifications()
        {
            var model = new NotificationsVM
            {
                rows = _db.notifications.Where(s=>s.ToUserId== _userManager.GetUserId(User))
                .Select(a => new NotificationsVM.Row
                {
                    NotId=a.Id,
                    From=a.FromUserName,
                    What=a.NotiBody,
                    IsRead=a.IsRead,
                    Detail=a.Message,
                    Date=a.CreatedDate.ToString("dd.MMMM.yy")
                }).ToList()
            };

            return View(model);
        }
        public JsonResult GetNotifications(bool bIsGetOnlyUnread = false)
        {
            var nToUserId = _userManager.GetUserId(User);
            _oNotifications = new List<Notification>();
            _oNotifications = _nootiService.GetNotifications(nToUserId, bIsGetOnlyUnread);
            return Json(_oNotifications);
        }
        public IActionResult IsItRead(int id)
        {
            var x = _db.notifications.Find(id);
            if (x.IsRead == false)
                x.IsRead = true;

            _db.Update(x);
            _db.SaveChanges();

            return RedirectToAction("AllNotifications");
        }
    }
}
