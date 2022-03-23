using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.Domain.DataModels;
using Api = VDS.Domain.ApiModels;

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
        public List<Api.CompanyHeader> GetCompanies()
        {
            using (var context = new DataStoreContext())
            {
                return context.Companies.Select(c => new Api.CompanyHeader
                {
                    Id = c.CompanyId,
                    Code = c.CompanyCode,
                    EmployeeCount = c.Employees.Count(),
                    Description = c.CompanyDescription
                }).ToList();
            }
        }

        public Api.Company GetCompanyById(int companyId)
        {
            using (var context = new DataStoreContext())
            {
                return context.Companies.Where(c => c.CompanyId == companyId).ToList()
                    .Select(c => new Api.Company
                    {
                        CompanyHeader = MapCompanyToCompanyHeader(c),
                        Employees = MapEmployeesToEmployeeHeader(c.Employees).ToArray()
                    }).SingleOrDefault();
            }
        }

        public Api.Employee GetEmployeeByCompanyIdAndEmployeeNumber(int companyId, string employeeNumber)
        {
            List<Employee> employees;
            using (var context = new DataStoreContext())
            {
                employees = context.Employees.Where(e => e.CompanyId == companyId).ToList();
            }
            return employees.Where(e=>e.EmployeeNumber == employeeNumber)
                .Select(e => new Api.Employee
            {
                EmployeeHeader = MapEmployeesToEmployeeHeader(new List<Employee> { e }).FirstOrDefault(),
                Email = e.EmployeeEmail,
                Department = e.EmployeeDepartment,
                HireDate = e.HireDate,
                // I would consider changing the model to fill the ancestors during load
                Managers = ConstructEmployeeHeadersAncestors(employees, e.ManagerEmployeeNumber).ToArray()
            }).SingleOrDefault();
        }

        private Api.CompanyHeader MapCompanyToCompanyHeader(Company c)
        {
            return new Api.CompanyHeader
            {
                Id = c.CompanyId,
                Code = c.CompanyCode,
                EmployeeCount = c.Employees.Count(),
                Description = c.CompanyDescription
            };
        }

        private IEnumerable<Api.EmployeeHeader> ConstructEmployeeHeadersAncestors(List<Employee> employees, string managerEmployeeNumber)
        {
            var employeeHeaders = new List<Api.EmployeeHeader>();
            ConstructEmployeeHeadersAncestorsRecursive(employeeHeaders, employees, managerEmployeeNumber);
            return employeeHeaders;
        }

        private void ConstructEmployeeHeadersAncestorsRecursive(List<Api.EmployeeHeader> employeeHeaders, List<Employee> employees, string managerEmployeeNumber)
        {
            if (string.IsNullOrWhiteSpace(managerEmployeeNumber))
                return;
            var manager = employees.Where(e => e.EmployeeNumber == managerEmployeeNumber).SingleOrDefault();
            var employeeHeader = new Api.EmployeeHeader
            {
                EmployeeNumber = manager.EmployeeNumber,
                FullName = GetEmployeeFullName(manager.EmployeeFirstName, manager.EmployeeLastName)
            };
            employeeHeaders.Add(employeeHeader);


            ConstructEmployeeHeadersAncestorsRecursive(employeeHeaders, employees, manager.ManagerEmployeeNumber);
        }

        private IEnumerable<Api.EmployeeHeader> MapEmployeesToEmployeeHeader(ICollection<Employee> employees)
        {
            foreach (var employee in employees)
            {
                yield return new Api.EmployeeHeader
                {
                    EmployeeNumber = employee.EmployeeNumber,
                    FullName = GetEmployeeFullName(employee.EmployeeFirstName, employee.EmployeeLastName)
                };
            }
        }

        private string GetEmployeeFullName(string employeeFirstName, string employeeLastName)
        {
            return $"{employeeFirstName} {employeeLastName}";
        }
    }
}
