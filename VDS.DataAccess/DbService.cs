using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.Domain.DataModels;

namespace VDS.DataAccess
{
    public class DbService : IDbService
    {
        public void ClearDataStore()
        {
            using (var context = new DataStoreContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Employees]"
                    + "DELETE FROM [Companies]"
                    + "DBCC CHECKIDENT ([Companies], RESEED, 0)");
            }
        }

        public void PopulateNewDataStore(HashSet<Company> companies)
        {
            using (var context = new DataStoreContext())
            {
                context.Companies.AddRange(companies);
                context.SaveChanges();
            }
        }
    }
}
