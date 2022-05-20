﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoArquitectura.Models;

namespace ProyectoArquitectura.Controllers.DEV
{
    public class ExcesoNutricionalsController : Controller
    {
        private DBNutricion db = new DBNutricion();

        // GET: ExcesoNutricionals
        public ActionResult Index()
        {
            var excesoNutricional = db.ExcesoNutricional.Include(e => e.NutrienteMaximo).Include(e => e.Patologia);
            return View(excesoNutricional.ToList());
        }

        // GET: ExcesoNutricionals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExcesoNutricional excesoNutricional = db.ExcesoNutricional.Find(id);
            if (excesoNutricional == null)
            {
                return HttpNotFound();
            }
            return View(excesoNutricional);
        }

        // GET: ExcesoNutricionals/Create
        public ActionResult Create()
        {
            ViewBag.IdMaximo = new SelectList(db.NutrienteMaximo, "IdMaximo", "IdMaximo");
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre");
            return View();
        }

        // POST: ExcesoNutricionals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdExceso,IdMaximo,IdPatologia")] ExcesoNutricional excesoNutricional)
        {
            if (ModelState.IsValid)
            {
                db.ExcesoNutricional.Add(excesoNutricional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMaximo = new SelectList(db.NutrienteMaximo, "IdMaximo", "IdMaximo", excesoNutricional.IdMaximo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", excesoNutricional.IdPatologia);
            return View(excesoNutricional);
        }

        // GET: ExcesoNutricionals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExcesoNutricional excesoNutricional = db.ExcesoNutricional.Find(id);
            if (excesoNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMaximo = new SelectList(db.NutrienteMaximo, "IdMaximo", "IdMaximo", excesoNutricional.IdMaximo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", excesoNutricional.IdPatologia);
            return View(excesoNutricional);
        }

        // POST: ExcesoNutricionals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdExceso,IdMaximo,IdPatologia")] ExcesoNutricional excesoNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(excesoNutricional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMaximo = new SelectList(db.NutrienteMaximo, "IdMaximo", "IdMaximo", excesoNutricional.IdMaximo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", excesoNutricional.IdPatologia);
            return View(excesoNutricional);
        }

        // GET: ExcesoNutricionals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExcesoNutricional excesoNutricional = db.ExcesoNutricional.Find(id);
            if (excesoNutricional == null)
            {
                return HttpNotFound();
            }
            return View(excesoNutricional);
        }

        // POST: ExcesoNutricionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExcesoNutricional excesoNutricional = db.ExcesoNutricional.Find(id);
            db.ExcesoNutricional.Remove(excesoNutricional);
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
