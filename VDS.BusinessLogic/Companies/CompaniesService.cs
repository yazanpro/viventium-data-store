using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.DataAccess;
using VDS.Domain.ApiModels;

namespace VDS.BusinessLogic.Companies
{
    public class CompaniesService : ICompaniesService
    {
        private IDbService _dbService;

        public CompaniesService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public List<CompanyHeader> GetCompanies()
        {
            return _dbService.GetCompanies();
        }

        public Company GetCompanyById(int companyId)
        {
            return _dbService.GetCompanyById(companyId);
        }

        public Employee GetEmployeeByEmpNumAndCompanyId(int companyId, string employeeNumber)
        {
            return _dbService.GetEmployeeByCompanyIdAndEmployeeNumber(companyId, employeeNumber);
        }
    }
}
