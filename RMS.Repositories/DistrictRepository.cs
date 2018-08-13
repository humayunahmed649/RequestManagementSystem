using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;
using RMS.Repositories.Base;
using RMS.Repositories.Contracts;

namespace RMS.Repositories
{
    public class DistrictRepository : Repository<District>,IDistrictRepository
    {
        public DistrictRepository(DbContext db) : base(db)
        {
        }

        public ICollection<District> GetAllDistrict()
        {
            return db.Set<District>().ToList();
        }

        public ICollection<District> GetDistrictsById(int id)
        {
            return db.Set<District>().Where(c => c.DivisionId == id).ToList();
        }  
    }
}
