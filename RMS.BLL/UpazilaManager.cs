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
    public class UpazilaManager:Manager<Upazila>,IUpazilaManager
    {
        private IUpazilaRepository _upazilaRepository;
        public UpazilaManager(IUpazilaRepository repository) : base(repository)
        {
            this._upazilaRepository = repository;
        }

        public ICollection<Upazila> GetAllUpazila()
        {
            return _upazilaRepository.GetAllUpazila();
        }

        public ICollection<Upazila> GetUpazilasById(int id)
        {
            return _upazilaRepository.GeUpazilasById(id);
        }
    }
}
