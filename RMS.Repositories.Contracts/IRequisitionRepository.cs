using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.App.ViewModels.Report;
using RMS.Models.EntityModels;

namespace RMS.Repositories.Contracts
{
    public interface IRequisitionRepository:IRepository<Requisition>
    {
        ICollection<Requisition> GetAllWithEmployee();
        ICollection<Requisition> GetAllRequisitionByEmployeeId(int id);


    }
}
