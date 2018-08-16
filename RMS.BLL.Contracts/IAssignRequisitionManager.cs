using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.BLL.Contracts
{
    public interface IAssignRequisitionManager:IManager<AssignRequisition>
    {
        ICollection<AssignRequisition> GetAllWithInformation();
        AssignRequisition SearchByName(string searchByText);

    }
}
