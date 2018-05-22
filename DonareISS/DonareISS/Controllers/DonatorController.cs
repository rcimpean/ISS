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
    public class DonatorController : Controller
    {
        private ISSEntities db = new ISSEntities();

        // GET: Donator
        [ValidationSession("Donator")]
        public ActionResult Index()
        {
            var user = (Utilizator)Session["Utilizator"];
            return View(db.Donator.ToList().First(x => x.Utilizator.Id_Utilizator == user.Id_Utilizator));
        }

        // GET: Donator/Details/5
        [ValidationSession("Donator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donator donator = db.Donator.Find(id);
            if (donator == null)
            {
                return HttpNotFound();
            }
            return View(donator);
        }

        // GET: Donator/Create
        [ValidationSession("Donator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationSession("Donator")]
        public ActionResult Create([Bind(Include = "Id_Donator")] Donator donator)
        {
            if (ModelState.IsValid)
            {
                db.Donator.Add(donator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donator);
        }

        // GET: Donator/Edit/5
        [ValidationSession("Donator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donator donator = db.Donator.Find(id);
            if (donator == null)
            {
                return HttpNotFound();
            }
            return View(donator);
        }

        // POST: Donator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationSession("Donator")]
        public ActionResult Edit([Bind(Include = "Id_Donator")] Donator donator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donator);
        }

        // GET: Donator/Delete/5
        [ValidationSession("Donator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donator donator = db.Donator.Find(id);
            if (donator == null)
            {
                return HttpNotFound();
            }
            return View(donator);
        }

        // POST: Donator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ValidationSession("Donator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Donator donator = db.Donator.Find(id);
            db.Donator.Remove(donator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ValidationSession("Donator")]
        public ActionResult Chestionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationSession("Donator")]
        public ActionResult Chestionar([Bind(Include = "Greutate,InterventiiChirurgicale,Puls,Tensiune,Destinatar,Boli")] ChestionarDonare chestionar)
        {
            var user = (Utilizator)Session["Utilizator"];
            
            if (chestionar.Greutate != null && chestionar.Puls != null && chestionar.Tensiune != null)
            {
                List<char> Boli = Request.Form["Boli"].ToList();
                for (int i = 0; i < Boli.Count; i++)
                {
                    if (Boli[i] != 44)
                        chestionar.Boli.Add(db.Boli.Find(Boli[i] - 48));
                }

                db.ChestionarDonare.Add(chestionar);
                chestionar.Donator = db.Donator.Where(x => x.Utilizator.Id_Utilizator == user.Id_Utilizator).First();
                db.SaveChanges();
                
                ViewBag.Msg = "Chestionarul a fost inregistrat cu succes!";
                return View("SuccessChestionar");
            }
            return View(chestionar);
        }

        [ChildActionOnly]
        [ValidationSession("Donator")]
        public ActionResult DropDownListBoli()
        {
            var boli = db.Boli.Select(b => new {
                BoliId = b.Id_Boala,
                Denumire = b.Denumire
            }).ToList();
            ViewData["ListaBoli"] = new MultiSelectList(boli, "BoliId", "Denumire");

            return PartialView();
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
