using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VDS.BusinessLogic.DataStore;

namespace ViventiumDataStore.Controllers
{
    public class DataStoreController : ApiController
    {
        private readonly IDataStoreService _dataStoreService;

        public DataStoreController(IDataStoreService dataStoreService)
        {
            _dataStoreService = dataStoreService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csv">Add '=' before your CSV content and pick "application/x-www-form-urlencoded" as the parameter content type.</param>
        [Route("DataStore")]
        [HttpPost]
        public IHttpActionResult DataStore([FromBody] string csv)
        {
            try
            {
                _dataStoreService.ClearAndImportDataStore(csv);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
