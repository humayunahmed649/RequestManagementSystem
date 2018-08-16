using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.Base;
using RMS.BLL.Contracts;
using RMS.Models.EntityModels;
using RMS.Repositories.Contracts;

namespace RMS.BLL
{
    public class EmployeeManager:Manager<Employee>,IEmployeeManager
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeManager(IEmployeeRepository repository) : base(repository)
        {
            this._employeeRepository = repository;
        }

        public ICollection<Employee> SearchByText(string searchText)
        {
            return _employeeRepository.SearchByText(searchText);
        }

        public ICollection<Employee> GetAllDriver()
        {
            return _employeeRepository.GetAllDriver();
        }
    }
}
