using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.App.ViewModels
{
    public class EmployeeTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public EmployeeTypes(int id,string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public EmployeeTypes()
        {
        }

        public IEnumerable SetTypes()
        {
                List<EmployeeTypes> employeeTypeses = new List<EmployeeTypes>();
                employeeTypeses.Add(new EmployeeTypes(Id = 1, Name = "Driver"));
                employeeTypeses.Add(new EmployeeTypes(Id = 2, Name = "Pion"));
            employeeTypeses.Add(new EmployeeTypes(Id = 3, Name = "Employee"));
            return employeeTypeses;
        }
    }  
}