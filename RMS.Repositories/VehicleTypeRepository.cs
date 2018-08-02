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
        public RmsDbContext db
        {
            get
            {
                return db as RmsDbContext;
            }
        }
        public VehicleTypeRepository(DbContext db) : base(db)
        {
        }
    }
}
