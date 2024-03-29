﻿using System;
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
    public class EmployeeTypeManager:Manager<EmployeeType>,IEmployeeTypeManager
    {
        private IEmployeeTypeRepository _employeeTypeRepository;
        public EmployeeTypeManager(IEmployeeTypeRepository repository) : base(repository)
        {
            this._employeeTypeRepository = repository;
        }

    }
}
