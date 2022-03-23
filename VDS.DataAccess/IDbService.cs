using System.Collections.Generic;
using VDS.Domain.DataModels;
using Api = VDS.Domain.ApiModels;

namespace VDS.DataAccess
{
    public interface IDbService
    {
        void ClearDataStore();
        void PopulateNewDataStore(HashSet<Company> companies);
        List<Api.CompanyHeader> GetCompanies();
        Api.Company GetCompanyById(int companyId);
        Api.Employee GetEmployeeByCompanyIdAndEmployeeNumber(int companyId, string employeeNumber);
    }
}