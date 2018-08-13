using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface IUpazilaManager:IManager<Upazila>
    {
        ICollection<Upazila> GetAllUpazila();
        ICollection<Upazila> GetUpazilasById(int id);
    }
}
