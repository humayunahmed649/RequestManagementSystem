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
    public class DesignationManager:Manager<Designation>,IDesignationManager
    {
        private IDesignationRepository _designationRepository;
        public DesignationManager(IDesignationRepository repository) : base(repository)
        {
            this._designationRepository = repository;
        }

        public ICollection<Designation> SearchByText(string searchText)
        {
            return _designationRepository.SearchByText(searchText);
        }
    }
}
