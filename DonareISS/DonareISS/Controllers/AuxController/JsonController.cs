using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DonareISS.Controllers.AuxController
{
    public class JsonController : Controller
    {
        private ISSEntities db = new ISSEntities();
        // GET: Json
        [HttpGet]
        public JsonResult GetOperators()
        {
            //var js = JsonConvert.SerializeObject(db.OperatorCentru.ToList());//, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var js = db.OperatorCentru.ToList();
            var Adrese = new List<string>();
            foreach (var item in js)
            {
                Adrese.Add(item.Adresa);
            }
            return Json(Adrese, JsonRequestBehavior.AllowGet);
        }
    }
}