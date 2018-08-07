using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.DatabaseContext;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class VehicleTypeRepository:Repository<VehicleType>,IVehicleTypeRepository
    {
       
        public VehicleTypeRepository(DbContext db) : base(db)
        {
        }

        public ICollection<VehicleType> SearchByType(string searchTextVehicleTypes)
        {
            return db.Set<VehicleType>().Where(c => c.Name.StartsWith(searchTextVehicleTypes)).ToList();
        }
    }
}
