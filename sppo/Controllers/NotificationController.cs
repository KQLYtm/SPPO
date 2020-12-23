using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using sppo.Data;
using sppo.Interface;
using SPPO.EntityModels;

namespace sppo.Controllers
{
    public class NotificationController : Controller
    {
        private readonly MyContext _db;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;

        public NotificationController(IHubContext<NotificationHub> notificationHubContext,
            IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager,
            MyContext db)
        {
            _notificationHubContext = notificationHubContext;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _db = db;
        }
        public IActionResult SendAll()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendAll(Notifikacije model)
        {
            // send all noticiations   
            await _notificationHubContext.Clients.All.SendAsync("sendToUser", model.Naslov, model.Sadrzaj);
            return View();
        }

        public IActionResult SendToSpecificUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SendToSpecificUser(Notifikacije model)
        {
            var connections = _userConnectionManager.GetUserConnections(model.userId);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", model.Naslov, model.Sadrzaj);
                }
            }
            return View();
        }

    }
}
