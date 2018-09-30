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
    public class VehicleRepository:Repository<Vehicle>,IVehicleRepository
    {
        
        public VehicleRepository(DbContext db) : base(db)
        {
        }

        public override ICollection<Vehicle> GetAll()
        {
            return db.Set<Vehicle>().Include(c => c.VehicleType).AsNoTracking().ToList();
        }

        public override Vehicle FindById(int id)
        {
            return db.Set<Vehicle>().Where(c=>c.Id==id).Include(c=>c.VehicleType).AsNoTracking().SingleOrDefault();
        }
    }
}
