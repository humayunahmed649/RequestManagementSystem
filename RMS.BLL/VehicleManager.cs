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
    public class VehicleManager:Manager<Vehicle>,IVehicleManager
    {
        private IVehicleRepository _vehicleRepository;
        public VehicleManager(IVehicleRepository repository) : base(repository)
        {
            this._vehicleRepository = repository;
        }

        public ICollection<Vehicle> SearchByText(string searchText)
        {
            return _vehicleRepository.SearchByText(searchText);
        }
    }
}
