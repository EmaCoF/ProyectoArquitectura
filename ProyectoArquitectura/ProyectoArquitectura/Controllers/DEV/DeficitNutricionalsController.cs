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
    public class DeficitNutricionalsController : Controller
    {
        private DBNutricion db = new DBNutricion();

        // GET: DeficitNutricionals
        public ActionResult Index()
        {
            var deficitNutricional = db.DeficitNutricional.Include(d => d.NutrienteMinimo).Include(d => d.Patologia);
            return View(deficitNutricional.ToList());
        }

        // GET: DeficitNutricionals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            if (deficitNutricional == null)
            {
                return HttpNotFound();
            }
            return View(deficitNutricional);
        }

        // GET: DeficitNutricionals/Create
        public ActionResult Create()
        {
            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "IdMinimo");
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre");
            return View();
        }

        // POST: DeficitNutricionals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDeficit,IdMinimo,IdPatologia")] DeficitNutricional deficitNutricional)
        {
            if (ModelState.IsValid)
            {
                db.DeficitNutricional.Add(deficitNutricional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "IdMinimo", deficitNutricional.IdMinimo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", deficitNutricional.IdPatologia);
            return View(deficitNutricional);
        }

        // GET: DeficitNutricionals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            if (deficitNutricional == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "IdMinimo", deficitNutricional.IdMinimo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", deficitNutricional.IdPatologia);
            return View(deficitNutricional);
        }

        // POST: DeficitNutricionals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDeficit,IdMinimo,IdPatologia")] DeficitNutricional deficitNutricional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deficitNutricional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMinimo = new SelectList(db.NutrienteMinimo, "IdMinimo", "IdMinimo", deficitNutricional.IdMinimo);
            ViewBag.IdPatologia = new SelectList(db.Patologia, "IdPatologia", "Nombre", deficitNutricional.IdPatologia);
            return View(deficitNutricional);
        }

        // GET: DeficitNutricionals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            if (deficitNutricional == null)
            {
                return HttpNotFound();
            }
            return View(deficitNutricional);
        }

        // POST: DeficitNutricionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeficitNutricional deficitNutricional = db.DeficitNutricional.Find(id);
            db.DeficitNutricional.Remove(deficitNutricional);
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
