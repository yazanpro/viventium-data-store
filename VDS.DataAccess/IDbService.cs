using System.Collections.Generic;
using VDS.Domain.DataModels;

namespace VDS.DataAccess
{
    public interface IDbService
    {
        void ClearDataStore();
        void PopulateNewDataStore(HashSet<Company> companies);
    }
}