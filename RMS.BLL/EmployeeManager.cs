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
        IAddressManager _addressManager;
        public EmployeeManager(IEmployeeRepository repository, IAddressManager addressManager) : base(repository)
        {
            this._employeeRepository = repository;
            this._addressManager = addressManager;
        }

        public ICollection<Employee> SearchByText(string searchText)
        {
            return _employeeRepository.SearchByText(searchText);
        }

        public ICollection<Employee> GetAllDriver()
        {
            return _employeeRepository.GetAllDriver();
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public override bool Update(Employee entity)
        {
          if(entity.Addresses!=null)
            {
                var addressIdList = entity.Addresses.Select(c => c.Id);
                var existingEmployee = FindById(entity.Id);
                var updateableAddresses = entity.Addresses.Where(c => c.Id > 0);
                var addeableAddresses = entity.Addresses.Where(c=>c.Id == 0);
                var deleteableAddresses = existingEmployee.Addresses.Where(c => !addressIdList.Contains(c.Id));

                if(addeableAddresses!=null)
                {
                    _addressManager.AddOrUpdate(addeableAddresses.ToList());
                }
                if(updateableAddresses!=null)
                {
                    _addressManager.AddOrUpdate(updateableAddresses.ToList());
                }      

            }

            return _Repository.Update(entity);
        }
    }
}
