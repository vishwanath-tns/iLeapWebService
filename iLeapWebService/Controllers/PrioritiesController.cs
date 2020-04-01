using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace iLeapWebService.Controllers
{
    public class StackedData
    {
        public List<int> data { get; set; }
        public string label { get; set; }

        public string Stack { get; set; }
    }
    public class WOPriority
    {
        public string MainCategory { get; set; }
        public string Priority { get; set; }
        public int PC { get; set; }
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PrioritiesController : ApiController
    {

        // GET: api/Priorities

        [HttpGet]
        public IEnumerable<WOPriority> Get()
        {
            using (iLeapRepoEntities entities = new iLeapRepoEntities())
            {
                int test;
                test = 0;
                
                var res = from MWO in entities.TAKAFUL_MDL_MaintenanceWorkOrder
                          group MWO by new
                          {
                              MWO.MainCategory,
                              MWO.Priority
                          } into g
                          select new
                          {
                              aMainCatergory = g.Key.MainCategory,
                              aPriority = g.Key.Priority,
                              aPC = g.Count()
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



                List<WOPriority> WOPriorities = new List<WOPriority>();
                foreach (var wop in res)
                {
                    WOPriority p = new WOPriority();
                    p.MainCategory = wop.aMainCatergory;
                    p.Priority = wop.aPriority;
                    p.PC = wop.aPC;
                    WOPriorities.Add(p);
                }

                return WOPriorities;


            }

            //List<StackedData> sdList = new List<StackedData>();
            //StackedData sd = new StackedData();
            //sd.data = new List<int>();
            //int[] marks = new int[7] { 150, 59, 80, 100, 56, 55, 40 };
            //sd.data = marks.OfType<int>().ToList();
            //sd.label = "Priority Test";
            //sd.Stack = "a";
            //sdList.Add(sd);

            //StackedData sd2 = new StackedData();
            //sd2.data = new List<int>();
            //int[] marks2 = new int[7] { 28, 48, 40, 19, 86, 27, 90 };
            //sd2.data = marks2.OfType<int>().ToList();
            //sd2.label = "Priority Medium";
            //sd2.Stack = "a";
            //sdList.Add(sd2);

            //StackedData sd3 = new StackedData();
            //sd3.data = new List<int>();
            //int[] marks3 = new int[7] { 28, 48, 40, 19, 86, 27, 90 };
            //sd3.data = marks3.OfType<int>().ToList();
            //sd3.label = "Priority High";
            //sd3.Stack = "a";
            //sdList.Add(sd3);

            //StackedData sd4 = new StackedData();
            //sd4.data = new List<int>();
            //int[] marks4 = new int[7] { 28, 58, 50, 59, 66, 87, 90 };
            //sd4.data = marks4.OfType<int>().ToList();
            //sd4.label = "Priority Highest";
            //sd4.Stack = "a";
            //sdList.Add(sd4);

            //return sdList;
        }
    }
}
