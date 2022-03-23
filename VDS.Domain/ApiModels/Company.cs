using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Domain.ApiModels
{
    public class Company
    {
        public CompanyHeader CompanyHeader { get; set; }
        public EmployeeHeader[] Employees { get; set; }
    }
}
