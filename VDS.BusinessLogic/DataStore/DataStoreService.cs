using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.DataAccess;
using VDS.Domain.DataModels;

namespace VDS.BusinessLogic.DataStore
{
    public class DataStoreService : IDataStoreService
    {
        private readonly IDbService _dbService;

        public DataStoreService(IDbService dbService)
        {
            _dbService = dbService;
        }
        public void ClearAndImportDataStore(string csv)
        {
            var companies = ConvertCsvToModel(csv);
            _dbService.ClearDataStore();
            _dbService.PopulateNewDataStore(companies);
        }

        private HashSet<Company> ConvertCsvToModel(string csv)
        {
            using (var strReader = new StringReader(csv))
            using (var csvReader = new CsvReader(strReader, CultureInfo.InvariantCulture))
            {
                var companies = new HashSet<Company>(new CompanyEqualityComparer());
                var companiesManagersMap = new Dictionary<int, HashSet<string>>();
                csvReader.Read();
                csvReader.ReadHeader();
                while (csvReader.Read())
                {
                    int companyId = csvReader.GetField<int>("CompanyId");
                    if (companyId <= 0)
                        throw new Exception("CompanyId cannot be less than or equal to 0");

                    // kind of a hacky approach to check if CompanyId exists in the hash set (after changing the comparer),
                    // I'm open to suggestions.
                    if (!companies.TryGetValue(new Company { CompanyId = companyId }, out Company company))
                    {
                        company = new Company
                        {
                            CompanyId = companyId,
                            CompanyCode = csvReader.GetField<string>("CompanyCode"),
                            CompanyDescription = csvReader.GetField<string>("CompanyDescription")
                        };
                        companies.Add(company);
                    }
                    AddEmployeeWithCheck(company, csvReader, companiesManagersMap);
                }
                ScanManagersIntegrity(companies, companiesManagersMap);

                return companies;
            }
        }

        private void ScanManagersIntegrity(HashSet<Company> companies, Dictionary<int, HashSet<string>> companiesManagersMap)
        {
            foreach(var managers in companiesManagersMap)
            {
                foreach(string manager in managers.Value)
                {
                    if (!companies.TryGetValue(new Company { CompanyId = managers.Key }, out Company company))
                        throw new Exception($"Company {managers.Key} is missing");

                    if (!company.Employees.Contains(new Employee { EmployeeNumber = manager, CompanyId = company.CompanyId }))
                    {
                        throw new Exception($"Manager {manager} is not found as an employee in company {company.CompanyId}");
                    }
                }
            }
        }

        private bool AddEmployeeWithCheck(Company company, CsvReader csvReader, Dictionary<int, HashSet<string>> companiesManagersMap)
        {
            string employeeNumber = csvReader.GetField<string>("EmployeeNumber");
            if (string.IsNullOrWhiteSpace(employeeNumber))
                throw new Exception("EmployeeNumber cannot be empty");

            if (company.Employees == null)
                company.Employees = new HashSet<Employee>(new EmployeeEqualityComparer());

            // check if the employee is an existing employee of the same company
            if (company.Employees.Contains(new Employee { EmployeeNumber = employeeNumber, CompanyId = company.CompanyId }))
                throw new Exception($"Employee number '{employeeNumber}' should be unique per company '{company.CompanyId}'");

            string managerEmployeeNumber = csvReader.GetField<string>("ManagerEmployeeNumber");

            if(!string.IsNullOrWhiteSpace(managerEmployeeNumber))
            {
                if (companiesManagersMap.TryGetValue(company.CompanyId, out HashSet<string> managers))
                    managers.Add(managerEmployeeNumber); // duplicates will be ignored which is fine
                else
                {
                    var newManagers = new HashSet<string>();
                    newManagers.Add(managerEmployeeNumber);
                    companiesManagersMap.Add(company.CompanyId, newManagers);
                }
            }
            
            AddEmployeeWithoutCheck(company, csvReader, employeeNumber, managerEmployeeNumber);

            return true;
        }

        private void AddEmployeeWithoutCheck(Company company, CsvReader csvReader, string employeeNumber, string managerEmployeeNumber)
        {
            company.Employees.Add(new Employee
            {
                EmployeeNumber = employeeNumber,
                EmployeeFirstName = csvReader.GetField<string>("EmployeeFirstName"),
                EmployeeLastName = csvReader.GetField<string>("EmployeeLastName"),
                EmployeeEmail = csvReader.GetField<string>("EmployeeEmail"),
                EmployeeDepartment = csvReader.GetField<string>("EmployeeDepartment"),
                HireDate = csvReader.GetField<DateTime?>("HireDate"),
                ManagerEmployeeNumber = managerEmployeeNumber,
                CompanyId = company.CompanyId
            });
        }
    }
}
