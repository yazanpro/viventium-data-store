using System.Collections.Generic;
using VDS.Domain.ApiModels;

namespace VDS.BusinessLogic.Companies
{
    public interface ICompaniesService
    {
        List<CompanyHeader> GetCompanies();
        Company GetCompanyById(int companyId);
        Employee GetEmployeeByEmpNumAndCompanyId(int companyId, string employeeNumber);
    }
}