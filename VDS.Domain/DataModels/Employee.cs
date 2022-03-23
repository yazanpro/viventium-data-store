using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Domain.DataModels
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [MaxLength(15)]
        public string EmployeeNumber { get; set; }
        [MaxLength(255)]
        public string EmployeeFirstName { get; set; }
        [MaxLength(255)]
        public string EmployeeLastName { get; set; }
        [MaxLength(320)]
        public string EmployeeEmail { get; set; }
        [MaxLength(255)]
        public string EmployeeDepartment { get; set; }
        public DateTime? HireDate { get; set; }
        [MaxLength(15)]
        public string ManagerEmployeeNumber { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }

    public class EmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee emp1, Employee emp2)
        {
            if (emp1 == null && emp2 == null)
                return true;
            else if (emp1 == null || emp2 == null)
                return false;
            else if (emp1.EmployeeNumber == emp2.EmployeeNumber && emp1.CompanyId == emp2.CompanyId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Employee emp)
        {
            return $"{emp.CompanyId}-{emp.EmployeeNumber}".GetHashCode();
        }
    }
}
