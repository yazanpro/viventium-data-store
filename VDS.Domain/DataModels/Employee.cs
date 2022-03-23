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
}
