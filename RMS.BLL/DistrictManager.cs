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
    public class DistrictManager:Manager<District>,IDistrictManager
    {
        private IDistrictRepository _districtRepository;
        public DistrictManager(IDistrictRepository repository) : base(repository)
        {
            this._districtRepository = repository;
        }

        public ICollection<District> GetAllDistrict()
        {
            return _districtRepository.GetAllDistrict();
        }

        public ICollection<District> GetDistrictsById(int id)
        {
            return _districtRepository.GetDistrictsById(id);
        }
    }
}
