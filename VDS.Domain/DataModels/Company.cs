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

    public class CompanyEqualityComparer : IEqualityComparer<Company>
    {
        public bool Equals(Company comp1, Company comp2)
        {
            if (comp1 == null && comp2 == null)
                return true;
            else if (comp1 == null || comp2 == null)
                return false;
            else if (comp1.CompanyId == comp2.CompanyId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Company comp)
        {
            return comp.CompanyId;
        }
    }
}
