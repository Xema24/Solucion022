using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Datos.Datos;

namespace Principal.Controllers
{
    public class ProductController : Controller
    {
        private NorthWindW3DbContext db = new NorthWindW3DbContext();

        // GET: Product
        ////public ActionResult Index()
        ////{
        ////    var products = db.Products.Include(p => p.Categories).Include(p => p.suppliers);
        ////    return View(products.ToList());
        ////}
        public ActionResult Index(int? id , bool categorisupplier)
        {
            var products = db.Products.Include(p => p.Categories).Include(p => p.suppliers);
                if (id != 0 && id != null)
            {
                if (categorisupplier != null)
                {
                    if (categorisupplier == true)
                    {
                        products = products.Where(x => x.CategoryID == id);
                        if (products != null && products.Count() > 0)
                        {
                            ViewBag.Message = "Nombre de la categoria :" + products.FirstOrDefault().Categories.CategoryName
                                + "Descripcion :" + products.FirstOrDefault().Categories.Description;
                        }
                     
                    }
                    else
                    {
                        products = products.Where(x => x.supplierID == id);
                        if (products != null && products.Count() > 0)
                        {
                            ViewBag.Message = "Nombre " + products.FirstOrDefault().suppliers.supplierName
                         + " Nombre de contacto" + products.FirstOrDefault().suppliers.ContactName
                         + "Ciudad" + products.FirstOrDefault().suppliers.City
                         + "Pais" + products.FirstOrDefault().suppliers.Country
                         + "Codigo Postal" + products.FirstOrDefault().suppliers.PostalCode
                         + "Telefono" + products.FirstOrDefault().suppliers.Phone;
                        }
                    }
                }
             
            }
            return View(products.ToList());
        }
        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName");
            return View();
        }

        // POST: Product/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,supplierID,CategoryID,unit,Price")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", products.supplierID);
            return View(products);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", products.supplierID);
            return View(products);
        }

        // POST: Product/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,supplierID,CategoryID,unit,Price")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierName", products.supplierID);
            return View(products);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
