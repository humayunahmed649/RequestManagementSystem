using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface IDistrictManager:IManager<District>
    {
        ICollection<District> GetAllDistrict();
        ICollection<District> GetDistrictsById(int id);
    }
}
