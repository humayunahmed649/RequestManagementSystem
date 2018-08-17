using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.Repositories.Contracts
{
    public interface IRequisitionStatusRepository:IRepository<RequisitionStatus>
    {
        ICollection<RequisitionStatus> GetAllWithRequisitionDetails();
        ICollection<RequisitionStatus> GetAllStatusNew();
        ICollection<RequisitionStatus> GetAllStatusExecute();
    }
}
