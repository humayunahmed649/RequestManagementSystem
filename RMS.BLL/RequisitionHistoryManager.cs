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
    public class RequisitionHistoryManager:Manager<RequisitionHistory>,IRequisitionHistoryManager
    {
        private IRequisitionHistoryRepository _requisitionHistoryRepository;
        public RequisitionHistoryManager(IRequisitionHistoryRepository repository) : base(repository)
        {
            this._requisitionHistoryRepository = repository;
        }

        public RequisitionHistory FindByRequisitionId(int id)
        {
            return _requisitionHistoryRepository.FindByRequisitionId(id);
        }
    }
}
