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
    public class UpazilaRepository : Repository<Upazila>,IUpazilaRepository
    {
        public UpazilaRepository(DbContext db) : base(db)
        {
        }

        public ICollection<Upazila> GetAllUpazila()
        {
            return db.Set<Upazila>().AsNoTracking().ToList();
        }
        public ICollection<Upazila> GetUpazilasById(int id)
        {
            return db.Set<Upazila>().Where(c => c.DistrictId == id).ToList();
        }
    }
}
