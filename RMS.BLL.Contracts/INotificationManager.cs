using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface INotificationManager:IManager<Notification>
    {
        ICollection<Notification> GetNotificationsForController(string controllerViewStatus);
        ICollection<Notification> GetNotificationsForSender(string senderViewstatus, int id);
        Notification FindByRequisitionId(int id);
    }
}
