using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace iLeapWebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TotalWOController : ApiController
    {
        // GET api/TotalWO
        [HttpGet]
        public int Get()
        {
            using (iLeapRepoEntities entities = new iLeapRepoEntities())
            {
                var WO = entities.TAKAFUL_MDL_MaintenanceWorkOrder
                    .SqlQuery("select * from dbo.TAKAFUL_MDL_MaintenanceWorkOrder")
                    .ToList<TAKAFUL_MDL_MaintenanceWorkOrder>();

                return WO.ToList().Count;

            }
            
        }
    }
}
