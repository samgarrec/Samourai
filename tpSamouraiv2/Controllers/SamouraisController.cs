using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using tpSamouraiv2.Data;
using tpSamouraiv2.Models;

namespace tpSamouraiv2.Controllers
{
    public class SamouraisController : Controller
    {
        private tpSamouraiv2Context db = new tpSamouraiv2Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiVM vm = new SamouraiVM();
            vm.Arme = db.Armes.Select(

                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })

                .ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SamouraiVM vm)
        {
            Samourai samourai = new Samourai();

            if (ModelState.IsValid)
            {
                samourai.Nom = vm.Samourai.Nom;

                if (vm.IdArme != null)
                {
                    samourai.Arme = db.Armes.Find(vm.IdArme);

                }
                db.Samourais.Add(samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Samourais/Edit/{id}
        public ActionResult Edit(int? id)
        {
            SamouraiVM vm = new SamouraiVM();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Samourai samourai = db.Samourais.Find(id);
            vm.Samourai = samourai;
            vm.Arme =db.Armes.Select(

                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })

                .ToList();

            if (vm.IdArme==null){
                vm.IdArme = samourai.Arme.Id;

            }
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Samourais/Edit/{id}
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit(SamouraiVM vm)
        {
            if (ModelState.IsValid)
            {
                Samourai samourai = db.Samourais.FirstOrDefault(s => s.Id==vm.Samourai.Id);

                if (vm.IdArme != null)
                {
                    samourai.Arme = db.Armes.Find(vm.IdArme);
                }
                samourai.Nom = vm.Samourai.Nom;
                samourai.Force = vm.Samourai.Force;
                db.Entry(samourai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
