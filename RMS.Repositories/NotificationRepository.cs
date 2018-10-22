using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class NotificationRepository:Repository<Notification>,INotificationRepository
    {
        public NotificationRepository(DbContext db) : base(db)
        {
        }
        public override ICollection<Notification> GetAll()
        {
            return db.Set<Notification>().Include(c => c.Employee).Include(c=>c.Requisition).OrderByDescending(c=>c.Id).AsNoTracking().ToList();
        }

       

        public ICollection<Notification> GetNotificationsForController(string controllerViewStatus)
        {
           return db.Set<Notification>().Where(s=>s.ControllerViewStatus==controllerViewStatus).Include(c => c.Employee).Include(c => c.Requisition).OrderByDescending(c => c.Id).AsNoTracking().ToList();
        }

        public ICollection<Notification> GetNotificationsForSender(string senderViewstatus,int id)
        {
            return db.Set<Notification>().Where(s => s.SenderViewStatus == senderViewstatus && s.EmployeeId==id).Include(c => c.Employee).Include(c => c.Requisition).OrderByDescending(c => c.Id).AsNoTracking().ToList();
        }

        public Notification FindByRequisitionId(int id)
        {
            return
                db.Set<Notification>()
                    .Where(c => c.RequisitionId == id)
                    .Include(c => c.Requisition)
                    .Include(c => c.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();
        }

        public ICollection<Notification> GetAllNotificationByEmployeeId(int empId)
        {
            return db.Set<Notification>().Where(s=>s.EmployeeId == empId).Include(c => c.Employee).Include(c => c.Requisition).OrderByDescending(c => c.Id).AsNoTracking().ToList();
        }

        public ICollection<Notification> GetAllNotificationForController()
        {
            return db.Set<Notification>().Include(c => c.Employee).Include(c => c.Requisition).OrderByDescending(c => c.Id).AsNoTracking().ToList();
        }
    }
}
