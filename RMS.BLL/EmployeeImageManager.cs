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
    public class EmployeeImageManager:Manager<EmployeeImage>,IEmployeeImageManager
    {
        private IEmployeeImageRepository _employeeImageRepository;
        public EmployeeImageManager(IEmployeeImageRepository employeeImageRepository) : base(employeeImageRepository)
        {
            this._employeeImageRepository = employeeImageRepository;
        }
    }
}
