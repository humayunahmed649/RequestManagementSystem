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
    public class CancelRequisitionManager:Manager<CancelRequisition>,ICancelRequisitionManager
    {
        private ICancelRequisitionRepository _cancelRequisitionRepository;
        public CancelRequisitionManager(ICancelRequisitionRepository repository) : base(repository)
        {
            this._cancelRequisitionRepository = repository;
        }
    }
}
