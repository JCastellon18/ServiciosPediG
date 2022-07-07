using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiciosPediG.Models;

namespace ServiciosPediG.Controllers
{
    [Authorize]
    public class TipoEntregasController : Controller
    {
        private PediGModelContainer db = new PediGModelContainer();

        // GET: TipoEntregas
        public ActionResult Index()
        {
            return View(db.TipoEntregas.ToList());
        }

        // GET: TipoEntregas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEntrega tipoEntrega = db.TipoEntregas.Find(id);
            if (tipoEntrega == null)
            {
                return HttpNotFound();
            }
            return View(tipoEntrega);
        }

        // GET: TipoEntregas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEntregas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Codigo,NombreTipo,Descripcion")] TipoEntrega tipoEntrega)
        {
            if (ModelState.IsValid)
            {
                db.TipoEntregas.Add(tipoEntrega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoEntrega);
        }

        // GET: TipoEntregas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEntrega tipoEntrega = db.TipoEntregas.Find(id);
            if (tipoEntrega == null)
            {
                return HttpNotFound();
            }
            return View(tipoEntrega);
        }

        // POST: TipoEntregas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Codigo,NombreTipo,Descripcion")] TipoEntrega tipoEntrega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoEntrega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoEntrega);
        }

        // GET: TipoEntregas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEntrega tipoEntrega = db.TipoEntregas.Find(id);
            if (tipoEntrega == null)
            {
                return HttpNotFound();
            }
            return View(tipoEntrega);
        }

        // POST: TipoEntregas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoEntrega tipoEntrega = db.TipoEntregas.Find(id);
            db.TipoEntregas.Remove(tipoEntrega);
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
