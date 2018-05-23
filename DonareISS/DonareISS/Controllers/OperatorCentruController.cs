using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DonareISS;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace DonareISS.Controllers
{
    public class OperatorCentruController : Controller
    {
        private ISSEntities db = new ISSEntities();

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index/Login");
        }

        // GET: OperatorCentru
        public ActionResult Index()
        {
            var op = new OperatorCentru();
            op.Adresa = "piata unirii cluj";
            ViewBag.op = op;
            return View(db.OperatorCentru.ToList());
        }

        public JsonResult GetOperator()
        {
            var json = JsonConvert.SerializeObject(db.OperatorCentru.ToList(), Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                        });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: OperatorCentru/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperatorCentru operatorCentru = db.OperatorCentru.Find(id);
            if (operatorCentru == null)
            {
                return HttpNotFound();
            }
            return View(operatorCentru);
        }

        // GET: OperatorCentru/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperatorCentru/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Operator,Adresa")] OperatorCentru operatorCentru)
        {
            if (ModelState.IsValid)
            {
                db.OperatorCentru.Add(operatorCentru);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(operatorCentru);
        }

        // GET: OperatorCentru/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperatorCentru operatorCentru = db.OperatorCentru.Find(id);
            if (operatorCentru == null)
            {
                return HttpNotFound();
            }
            return View(operatorCentru);
        }

        // POST: OperatorCentru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Operator,Adresa")] OperatorCentru operatorCentru)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operatorCentru).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(operatorCentru);
        }

        // GET: OperatorCentru/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperatorCentru operatorCentru = db.OperatorCentru.Find(id);
            if (operatorCentru == null)
            {
                return HttpNotFound();
            }
            return View(operatorCentru);
        }

        // POST: OperatorCentru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperatorCentru operatorCentru = db.OperatorCentru.Find(id);
            db.OperatorCentru.Remove(operatorCentru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult CerereNoua()
        {
            IEnumerable<Cerere> cerere = db.Cerere.Where(x => x.Status.ToLower() == "inregistrata");

            if (cerere != null)
                return View(cerere);
            return View(new List<Cerere>());
        }

        public ActionResult EditCerere(int id)
        {
            return View(db.Cerere.Find(id));
        }

        [HttpPost]
        public ActionResult EditCerere([Bind(Include = "Celula_Ceruta,Cantitate_Ceruta")] Cerere cerere)
        {
            var user = (Utilizator)Session["Utilizator"];

            if (ModelState.IsValid)
            {
                cerere.Status = "procesata";
                var stoc = db.Stoc.Where(x => x.OperatorCentru.Utilizator.Id_Utilizator == user.Id_Utilizator).First();
                int numVal = Int32.Parse(cerere.Cantitate_Ceruta);

                if (cerere.Celula_Ceruta.ToLower() == "plasma")
                    stoc.CantitatePlasma = stoc.CantitatePlasma - numVal;
                else if (cerere.Celula_Ceruta.ToLower() == "trombocite")
                    stoc.CantitateTrombocite = stoc.CantitateTrombocite - numVal;
                else if (cerere.Celula_Ceruta.ToLower() == "globulerosii")
                    stoc.CantitateGlobuleRosii = stoc.CantitateGlobuleRosii - numVal;
                db.SaveChanges();
            }
            return View("CerereProcesata", db.Cerere.Where(x => x.Cantitate_Ceruta == cerere.Cantitate_Ceruta && x.Celula_Ceruta == cerere.Celula_Ceruta).First().Medic);
        }

        [ChildActionOnly]
        public ActionResult VeziStoc()
        {
            var user = (Utilizator)Session["Utilizator"];
            var ope = db.OperatorCentru.Where(x => x.Utilizator.Id_Utilizator == user.Id_Utilizator).First();

            if (ope.Stoc.First() != null)
                return View(ope.Stoc.First());

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
