using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.Domain.DataModels;

namespace VDS.DataAccess
{
    public class DataStoreContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DataStoreContext() : base("DataStoreConnection")
        {

        }
    }
}
