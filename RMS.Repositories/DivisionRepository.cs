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
    public class DivisionRepository : Repository<Division>,IDivisionRepository
    {


        public ICollection<Division> GetAllDivisions()
        {
            return db.Set<Division>().ToList();
        }

        public DivisionRepository(DbContext db) : base(db)
        {
        }

    }
}
