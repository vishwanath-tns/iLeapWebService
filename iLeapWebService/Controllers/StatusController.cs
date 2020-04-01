using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace iLeapWebService.Controllers
{
    public class WOStatus
    {
        public string Status { get; set; }
        public int NWO { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StatusController : ApiController
    {
        // GET api/Status
        [HttpGet]
        public IEnumerable<WOStatus> Get()
        {
            using (iLeapRepoEntities entities = new iLeapRepoEntities())
            {
                int test = 0;
                //var WO = entities.Database
                //    .SqlQuery<WOStatus>("select Status, count(*) as NWO from TAKAFUL_MDL_MaintenanceWorkOrder group by Status");

                var res = from MWO in entities.TAKAFUL_MDL_MaintenanceWorkOrder
                          group MWO by MWO.Status into g
                          select new
                          {
                              Status = g.Key,
                              NWO = g.Count()
                          };

                //var res = from WOL in entities.TAKAFUL_MDL_MainenanceWorkOrderList
                //          join tl in entities.TAKAFUL_MDL_Maintaintechnicianslist
                //          on WOL.TechnicianNameList_Value equals tl.Id.ToString()
                //          group WOL by WOL.TechnicianNameList into newGroup
                //          select new
                //          {
                //              TechnicianName = newGroup.Key,
                //              NumberOfWorkOrders = newGroup.Count()
                //          };



                List<WOStatus> WOStatus = new List<WOStatus>();
                foreach (var two in res)
                {
                    WOStatus t = new WOStatus();
                    t.Status = two.Status;
                    t.NWO = two.NWO;
                    WOStatus.Add(t);
                }

                return WOStatus;


            }

            //List<List<int>> listofLists = new List<List<int>>();
            //List<int> bars = new List<int>
            //{
            //    55, 25, 100
            //};
            //listofLists.Add(bars);
            //return listofLists;
        }
    }
}
