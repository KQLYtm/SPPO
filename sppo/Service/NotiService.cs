using Dapper;
using Microsoft.Data.SqlClient;
using sppo.Common;
using sppo.IService;
using SPPO.EntityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.Service
{
    public class NotiService : INotiService
    {
        List<Notification> _oNotifications = new List<Notification>();
        public List<Notification> GetNotifications(string nToUserID, bool bIsGetOnlyUnread)
        {
            _oNotifications = new List<Notification>();
            using (IDbConnection con = new SqlConnection(Global.ConnectionStrings))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var oNotis = con.Query<Notification>("SELECT * FROM notifications WHERE ToUserId='" + nToUserID + "'").ToList();
                if (oNotis != null && oNotis.Count() > 0)
                {
                    _oNotifications = oNotis;
                }
                return _oNotifications;
            }
        }
    }
}
