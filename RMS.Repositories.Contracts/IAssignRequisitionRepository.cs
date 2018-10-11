using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.App.ViewModels.Report;
using RMS.Models.EntityModels;
using RMS.Models.EntityViewModel;

namespace RMS.Repositories.Contracts
{
    public interface IAssignRequisitionRepository:IRepository<AssignRequisition>
    {
        ICollection<AssignRequisition> GetAllWithInformation();
        AssignRequisition  SearchByText(string searchByText);
        AssignRequisition GetVehicleStatus(int vehicleId);
        AssignRequisition GetDriverStatus(int driverId);
        ICollection<AssignRequisitionReportVM> GetRequisitionSummaryReport();
    }
}
