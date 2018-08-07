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
    public class DesignationRepository:Repository<Designation>,IDesignationRepository
    {
        
        public DesignationRepository(DbContext db) : base(db)
        {
        }

        public ICollection<Designation> SearchByTitle(string searchDesignationTitle)
        {
            return db.Set<Designation>().Where(c => c.Title.StartsWith(searchDesignationTitle)).ToList();
        }
    }
}
