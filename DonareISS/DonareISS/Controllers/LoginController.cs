using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace DonareISS.Controllers
{
    public class LoginController : Controller
    {
        private ISSEntities db = new ISSEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View(db.Utilizator.Find(1));
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
            var sex = Request.Form["Sex"];
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    db.Utilizator.Add(model);
                    db.SaveChanges();
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
