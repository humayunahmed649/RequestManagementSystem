using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Models.EntityModels;

namespace RMS.Repositories.Contracts
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
        ICollection<Employee> GetAllDriver();
        ICollection<Employee> GetAllEmployees();
        Employee FindByLoginId(int id);
    }
}
