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
    public class RequisitionStatusManager:Manager<RequisitionStatus>,IRequisitionStatusManager
    {
        private IRequisitionStatusRepository _requisitionStatusRepository;
        public RequisitionStatusManager(IRequisitionStatusRepository repository) : base(repository)
        {
            this._requisitionStatusRepository = repository;
        }


        public ICollection<RequisitionStatus> GetAllWithRequisitionDetails()
        {
            return _requisitionStatusRepository.GetAllWithRequisitionDetails();
        }
        public ICollection<RequisitionStatus> GetAllStatusNew()
        {
            return _requisitionStatusRepository.GetAllStatusNew();
        }

        public ICollection<RequisitionStatus> GetAllStatusExecute()
        {
            return _requisitionStatusRepository.GetAllStatusExecute();
        }

        public ICollection<RequisitionStatus> SearchByRequisitionId(string searchText)
        {
            return _requisitionStatusRepository.SearchByRequisitionId(searchText);
        }
    }
}
