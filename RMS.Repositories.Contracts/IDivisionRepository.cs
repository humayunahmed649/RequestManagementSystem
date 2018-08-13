using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.Repositories.Contracts
{
    public interface IDivisionRepository:IRepository<Division>
    {
         ICollection<Division> GetAllDivisions();
    }
}
