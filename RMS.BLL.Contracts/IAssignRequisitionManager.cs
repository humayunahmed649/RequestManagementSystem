using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.App.ViewModels.Report;
using RMS.Models.EntityModels;
using RMS.Models.EntityViewModel;
using RMS.Models.Report;

namespace RMS.BLL.Contracts
{
    public interface IAssignRequisitionManager:IManager<AssignRequisition>
    {
        ICollection<AssignRequisition> GetAllWithInformation();
        AssignRequisition SearchByName(string searchByText);
        AssignRequisition GetVehicleStatus(int vehicleId);
        AssignRequisition GetDriverStatus(int driverId);
        ICollection<AssignRequisitionReportVM> GetRequisitionSummaryReport();
        List<AssignReportVM> GetAssignRequisition();
    }
}
