using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace iLeapWebService.Controllers
{
    public class TechnicianWO
    {
        public string TechnicianNameList { get; set; }
        public int NWO { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        // GET: api/Values
        //[Produces("application/json")]
        public IEnumerable<TechnicianWO> Get()
        {
            using (iLeapRepoEntities entities = new iLeapRepoEntities())
            {
                int test = 0;
                var WO = entities.Database
                    .SqlQuery<TechnicianWO>("select wol.TechnicianNameList, count(*) as NWO  from TAKAFUL_MDL_MainenanceWorkOrderList wol join TAKAFUL_MDL_Maintaintechnicianslist tl on wol.TechnicianNameList_Value = CAST(tl.Id as nvarchar) group by wol.TechnicianNameList");

                //var res = from WOL in entities.TAKAFUL_MDL_MainenanceWorkOrderList
                //          join tl in entities.TAKAFUL_MDL_Maintaintechnicianslist
                //          on WOL.TechnicianNameList_Value equals tl.Id.ToString()
                //          group WOL by WOL.TechnicianNameList into newGroup
                //          select new
                //          {
                //              TechnicianName = newGroup.Key,
                //              NumberOfWorkOrders = newGroup.Count()
                //          };



                List < TechnicianWO > twoList = new List<TechnicianWO>();
                foreach (TechnicianWO two in WO)
                {
                    TechnicianWO t = new TechnicianWO();
                    t.TechnicianNameList = two.TechnicianNameList;
                    t.NWO = two.NWO;
                    twoList.Add(t);
                }

                return twoList;


            }

            //List<int> bars = new List<int>
            //{
            //    45,
            //    37,
            //    60,
            //    70
            //};
            //return bars;
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
