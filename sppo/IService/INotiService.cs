using SPPO.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sppo.IService
{
    public interface INotiService
    {
        List<Notification> GetNotifications(string nToUserID, bool bIsGetOnlyUnread);
    }
}
