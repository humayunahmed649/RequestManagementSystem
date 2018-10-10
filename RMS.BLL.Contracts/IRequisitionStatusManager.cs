using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface IRequisitionStatusManager:IManager<RequisitionStatus>
    {
        ICollection<RequisitionStatus> GetAllWithRequisitionDetails();
        ICollection<RequisitionStatus> GetAllStatusNew();
        ICollection<RequisitionStatus> GetAllStatusExecute();
        ICollection<RequisitionStatus> SearchByRequisitionId(string searchText);
        ICollection<RequisitionStatus> GetAllById(int id);
        RequisitionStatus FindByRequisitionId(int id);
        ICollection<RequisitionStatus> GetAllRequisitions();
        ICollection<RequisitionStatus> GetAllCheckOutCheckIn();
    }
}
