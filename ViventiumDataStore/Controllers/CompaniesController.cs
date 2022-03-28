using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VDS.BusinessLogic;
using VDS.BusinessLogic.Companies;

namespace ViventiumDataStore.Controllers
{
    public class CompaniesController : ApiController
    {
        private readonly ICompaniesService _companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [Route("Companies")]
        [HttpGet]
        public IHttpActionResult Companies()
        {
            return Ok(_companiesService.GetCompanies());
        }

        [Route("Companies/{companyId}")]
        [HttpGet]
        public IHttpActionResult CompanyById(int companyId)
        {
            return Ok(_companiesService.GetCompanyById(companyId));
        }

        [Route("Companies/{companyId}/Employees/{employeeNumber}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeByEmpNumAndCompanyId(int companyId, string employeeNumber)
        {
            return Ok(_companiesService.GetEmployeeByEmpNumAndCompanyId(companyId, employeeNumber));
        }
    }
}
