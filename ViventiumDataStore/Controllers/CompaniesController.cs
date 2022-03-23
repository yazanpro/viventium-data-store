using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VDS.BusinessLogic;

namespace ViventiumDataStore.Controllers
{
    public class CompaniesController : ApiController
    {
        [Route("Companies")]
        [HttpGet]
        public IHttpActionResult Companies()
        {
            return Ok();
        }

        [Route("Companies/{companyId}")]
        [HttpGet]
        public IHttpActionResult CompanyById(int companyId)
        {
            return Ok();
        }

        [Route("Companies/{companyId}/Employees/{employeeNumber}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeByEmpNumAndCompanyId(int companyId, string employeeNumber)
        {
            return Ok();
        }
    }
}
