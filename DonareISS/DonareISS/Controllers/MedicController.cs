using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DonareISS;
using DonareISS.Validation;

namespace DonareISS.Controllers
{
    public class MedicController : Controller
    {
        private ISSEntities db = new ISSEntities();

        // GET: Medic
        [ValidationSession("Medic")]
        public ActionResult Index()
        {
            Utilizator ut = (Utilizator)Session["Utilizator"];
            Medic m = db.Medic.Where(x => x.Utilizator.Id_Utilizator == ut.Id_Utilizator).First();
            /*Gandeste o descriere pentru Medic/Index (informatii de baza + linkuri)*/
            return View(m);
        }

        // GET: Medic/Details/5
        [ValidationSession("Medic")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medic.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }

        // GET: Medic/Create
        [ValidationSession("Medic")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationSession("Medic")]
        public ActionResult Create([Bind(Include = "Id_Medic,Spital")] Medic medic)
        {
            if (ModelState.IsValid)
            {
                db.Medic.Add(medic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medic);
        }

        // GET: Medic/Edit/5
        [ValidationSession("Medic")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medic.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }

        // POST: Medic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationSession("Medic")]
        public ActionResult Edit([Bind(Include = "Id_Medic,Spital")] Medic medic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medic);
        }

        // GET: Medic/Delete/5
        [ValidationSession("Medic")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medic.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }

        // POST: Medic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidationSession("Medic")]
        public ActionResult DeleteConfirmed(int id)
        {
            Medic medic = db.Medic.Find(id);
            db.Medic.Remove(medic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get: Medic/Pacienti/1
        [ValidationSession("Medic")]
        public ActionResult Pacienti(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Nu sa primit Medicul!!!");

            Medic medic = db.Medic.Find(id);

            if (medic == null)
                return HttpNotFound("Nu exista acest medic");

            return View(medic.Pacient);
        }

        [ValidationSession("Medic")]
        public ActionResult CerereSange()
        {
            return View();
        }

        [ChildActionOnly]
        [ValidationSession("Medic")]
        public ActionResult SelectPacienti()
        {
            var list = db.Pacient.ToList();
            List<SelectListItem> selectlist = new List<SelectListItem>();
            foreach (var item in list)
            {
                selectlist.Add(new SelectListItem { Text = item.Utilizator.Nume + ' ' + item.Utilizator.Prenume , Value = item.Id_Pacient.ToString()});
            }

            ViewData["ListaPacienti"] = selectlist;

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationSession("Medic")]
        public ActionResult CerereSange([Bind(Include = "Celula_Ceruta,Cantitate_Ceruta,SelectPacienti")] Cerere cerere)
        {
            string selectedPacientId = Request.Form["SelectPacienti"];
            var user = (Utilizator)Session["Utilizator"];

            if (ModelState.IsValid && cerere.Celula_Ceruta != null && cerere.Cantitate_Ceruta != null)
            {
                cerere.Status = "Inregistrata";
                cerere.Pacient = db.Pacient.Find(Convert.ToInt32(selectedPacientId));
                cerere.Medic = db.Medic.Where(x => x.Utilizator.Id_Utilizator == user.Id_Utilizator).First();
                db.Cerere.Add(cerere);
                db.SaveChanges();
                
                ViewBag.Msg = "Cererea a fost inregistrata cu succes!";
                return View("Success");
            }
            return View(cerere);
        }

        [ValidationSession("Medic")]
        public ActionResult Success()
        {
            return View();
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
