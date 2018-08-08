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
    public class VehicleTypeManager:Manager<VehicleType>,IVehicleTypeManager
    {
        private IVehicleTypeRepository _vehicleTypeRepository;
        public VehicleTypeManager(IVehicleTypeRepository repository) : base(repository)
        {
            this._vehicleTypeRepository = repository;
        }

        public ICollection<VehicleType> SearchByType(string searchTextVehicleTypes)
        {
            return _vehicleTypeRepository.SearchByType(searchTextVehicleTypes);
        }
    }
}
