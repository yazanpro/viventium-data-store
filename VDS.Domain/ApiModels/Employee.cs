using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Domain.ApiModels
{
    public class Employee
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime? HireDate { get; set; }
        public EmployeeHeader[] Managers { get; set; }
    }
}
