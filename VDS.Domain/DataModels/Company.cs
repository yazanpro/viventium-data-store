using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Domain.DataModels
{
    public class Company
    {
        public int CompanyId { get; set; }
        [MaxLength(255)]
        public string CompanyCode { get; set; }
        public string CompanyDescription { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
