using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.Base;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using RMS.Repositories.Contracts;

namespace RMS.BLL
{
    public class NotificationManager:Manager<Notification>,INotificationManager
    {
        private INotificationRepository _notificationRepository;
        public NotificationManager(INotificationRepository notificationRepository) : base(notificationRepository)
        {
            this._notificationRepository = notificationRepository;
        }

        public ICollection<Notification> GetNotificationsForController(string controllerViewStatus)
        {
            return _notificationRepository.GetNotificationsForController(controllerViewStatus);
        }

        public ICollection<Notification> GetNotificationsForSender(string senderViewstatus, int id)
        {
            return _notificationRepository.GetNotificationsForSender(senderViewstatus,id);
        }

        public Notification FindByRequisitionId(int id)
        {
            return _notificationRepository.FindByRequisitionId(id);
        }

        public ICollection<Notification> GetAllNotificationByEmployeeId(int empId)
        {
            return _notificationRepository.GetAllNotificationByEmployeeId(empId);
        }

        public ICollection<Notification> GetAllNotificationForController()
        {
            return _notificationRepository.GetAllNotificationForController();
        }
    }
}
