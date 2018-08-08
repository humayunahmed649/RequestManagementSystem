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
    public class DepartmentManager:Manager<Department>,IDepartmentManager
    {
        private IDepartmentRepository _departmentRepository;
        public DepartmentManager(IDepartmentRepository repository) : base(repository)
        {
            this._departmentRepository = repository;
        }

        

        public ICollection<Department> SearchByName(string searchText)
        {
            return _departmentRepository.SearchByName(searchText);
        }
    }
}
