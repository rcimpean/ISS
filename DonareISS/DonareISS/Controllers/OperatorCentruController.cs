using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DonareISS;

namespace DonareISS.Controllers
{
    public class OperatorCentruController : Controller
    {
        private ISSEntities db = new ISSEntities();

        // GET: OperatorCentru
        public ActionResult Index()
        {
            return View(db.OperatorCentru.ToList());
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
