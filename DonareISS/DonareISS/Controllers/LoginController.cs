﻿using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Security.Cryptography;
using DonareISS;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.Security;
using WebMatrix.WebData;
using System;
using System.Linq;

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
                return RedirectToAction("Index", ut.Functie.Replace(" ", ""));
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

        // GET: Login/Register
        public ActionResult Register()
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
        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Utilizator model)
        {
            var ConfirmareParola = Request.Form["pwd"];
            if (ModelState.IsValid && (model.Parola.Equals(ConfirmareParola)))
            {
                var PAROLA = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Parola));
                model.Parola = BitConverter.ToString(PAROLA).Replace("-", "").ToLower();
                if (model.Functie == "Operator")
                    model.Functie = "OperatorCentru";
                // Attempt to register the user
                try
                {
                    db.Utilizator.Add(model);
                    db.SaveChanges();
                    if (model.Functie == "Medic")
                    {
                        var Spital = Request.Form["Spital"];
                        var medic = new Medic();
                        medic.Spital = Spital;
                        medic.Utilizator = model;
                        db.Medic.Add(medic);
                        db.SaveChanges();
                    }
                    else if (model.Functie == "OperatorCentru")
                    {
                        var AdresaCentru = Request.Form["AdresaCentru"];
                        var operatorCentru = new OperatorCentru();
                        operatorCentru.Adresa = AdresaCentru;
                        operatorCentru.Utilizator = model;
                        db.OperatorCentru.Add(operatorCentru);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", e.StatusCode.ToString());
                }
            }
            

            // If we got this far, something failed, redisplay form
            return View(model);
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
