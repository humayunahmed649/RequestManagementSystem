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
    public class DivisionManager:Manager<Division>,IDivisionManager
    {
        private IDivisionRepository _divisionRepository;
        public DivisionManager(IDivisionRepository repository) : base(repository)
        {
            this._divisionRepository = repository;
        }


        public ICollection<Division> GetAllDivisions()
        {
            return _divisionRepository.GetAllDivisions();
        }
    }
}
