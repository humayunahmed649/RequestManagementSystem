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
    public class RequisitionManager:Manager<Requisition>,IRequisitionManager
    {
        private IRequisitionRepository _requisitionRepository;
        public RequisitionManager(IRequisitionRepository repository) : base(repository)
        {
            this._requisitionRepository = repository;
        }

        public ICollection<Requisition> GetAllWithEmployee()
        {
            return _requisitionRepository.GetAllWithEmployee();
        }

        
       
    }
}
