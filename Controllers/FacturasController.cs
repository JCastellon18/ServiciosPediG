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
    public class FacturasController : Controller
    {
        private PediGModelContainer db = new PediGModelContainer();

        // GET: Facturas
        public ActionResult Index()
        {
            var facturas = db.Facturas.Include(f => f.Cliente).Include(f => f.TipoEntrega).Include(f => f.TipoDePago).Include(f => f.EstadoFactura);
            return View(facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres");
            ViewBag.TipoEntregaId = new SelectList(db.TipoEntregas, "Id", "NombreTipo");
            ViewBag.TipoDePagoId = new SelectList(db.TipoDePagos, "Id", "Codigo");
            ViewBag.EstadoFacturaId = new SelectList(db.Reservaciones, "Id", "Codigo");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumeroFactura,FechaFactura,TotalNeto,TipoDePagoId,TipoEntregaId,ClienteId,DireccionPedido,EstadoFacturaId,FechaEntregaPedido,User")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Facturas.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres", factura.ClienteId);
            ViewBag.TipoEntregaId = new SelectList(db.TipoEntregas, "Id", "NombreTipo", factura.TipoEntregaId);
            ViewBag.TipoDePagoId = new SelectList(db.TipoDePagos, "Id", "Codigo", factura.TipoDePagoId);
            ViewBag.EstadoFacturaId = new SelectList(db.Reservaciones, "Id", "Codigo", factura.EstadoFacturaId);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres", factura.ClienteId);
            ViewBag.TipoEntregaId = new SelectList(db.TipoEntregas, "Id", "NombreTipo", factura.TipoEntregaId);
            ViewBag.TipoDePagoId = new SelectList(db.TipoDePagos, "Id", "Codigo", factura.TipoDePagoId);
            ViewBag.EstadoFacturaId = new SelectList(db.Reservaciones, "Id", "Codigo", factura.EstadoFacturaId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumeroFactura,FechaFactura,TotalNeto,TipoDePagoId,TipoEntregaId,ClienteId,DireccionPedido,EstadoFacturaId,FechaEntregaPedido,User")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombres", factura.ClienteId);
            ViewBag.TipoEntregaId = new SelectList(db.TipoEntregas, "Id", "NombreTipo", factura.TipoEntregaId);
            ViewBag.TipoDePagoId = new SelectList(db.TipoDePagos, "Id", "Codigo", factura.TipoDePagoId);
            ViewBag.EstadoFacturaId = new SelectList(db.Reservaciones, "Id", "Codigo", factura.EstadoFacturaId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Facturas.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Facturas.Find(id);
            db.Facturas.Remove(factura);
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
