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
    public class ArticulosController : Controller
    {
        private PediGModelContainer db = new PediGModelContainer();

        // GET: Articulos
        public ActionResult Index()
        {
            var articulos = db.Articulos.Include(a => a.Categoria);
            return View(articulos.ToList());
        }

        // GET: Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articulos.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Codigo");
            return View();
        }

        // POST: Articulos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreArticulo,Descripcion,Img,Precio,Cantidad,CategoriaId,User")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Articulos.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Codigo", articulo.CategoriaId);
            return View(articulo);
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articulos.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Codigo", articulo.CategoriaId);
            return View(articulo);
        }

        // POST: Articulos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreArticulo,Descripcion,Img,Precio,Cantidad,CategoriaId,User")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Codigo", articulo.CategoriaId);
            return View(articulo);
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articulos.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulo articulo = db.Articulos.Find(id);
            db.Articulos.Remove(articulo);
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
