using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using DonareISS;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace DonareISS.Controllers
{
    public class LoginController : Controller
    {


        private ISSEntities db = new ISSEntities();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Login


        public Utilizator VerificareUtilizator(string parolaCriptata, string email)
        {
            var TotiUtilizatorii = db.Utilizator.ToList();
            foreach (Utilizator ut in TotiUtilizatorii)
            {
                if (ut.Parola == parolaCriptata && ut.Email == email)
                {
                    return ut;
                }


            }
            return null;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index([Bind(Include = "Email,Parola")] Utilizator utilizator)
        {

            var PAROLA = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(utilizator.Parola));
            var ParolaCriptata = BitConverter.ToString(PAROLA).Replace("-", "").ToLower();
            Utilizator ut = new Utilizator();
            
            if ((ut = VerificareUtilizator(ParolaCriptata, utilizator.Email)) == null)
            {
                return Content("You have to create an account");
            }
            else
            {
                //return View("aici");
                Session["Utilizator"] = ut;
                return RedirectToAction("Index", ut.Functie);
               // return RedirectToAction("Index", "Home");

            }
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizator utilizator = db.Utilizator.Find(id);
            if (utilizator == null)
            {
                return HttpNotFound();
            }
            return View(utilizator);
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Utilizator,Nume,Prenume,Email,Varsta,Sex,CNP,Parola,Functie,Adresa")] Utilizator utilizator)
        {
            if (ModelState.IsValid)
            {
                db.Utilizator.Add(utilizator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utilizator);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizator utilizator = db.Utilizator.Find(id);
            if (utilizator == null)
            {
                return HttpNotFound();
            }
            return View(utilizator);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Utilizator,Nume,Prenume,Email,Varsta,Sex,CNP,Parola,Functie,Adresa")] Utilizator utilizator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilizator);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizator utilizator = db.Utilizator.Find(id);
            if (utilizator == null)
            {
                return HttpNotFound();
            }
            return View(utilizator);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizator utilizator = db.Utilizator.Find(id);
            db.Utilizator.Remove(utilizator);
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
