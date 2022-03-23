using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Domain.DataModels
{
    public class Employee
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeDepartment { get; set; }
        public DateTime HireDate { get; set; }
        public string ManagerEmployeeNumber { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
