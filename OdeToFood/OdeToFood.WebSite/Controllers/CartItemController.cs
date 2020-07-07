using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Data.Model;
using OdeToFood.Data.Services;

namespace OdeToFood.WebSite.Controllers
{
    public class CartItemController : Controller
    {
        private RestoDbContext db = new RestoDbContext();

        // GET: CartItem
        public ActionResult Index()
        {
            var cartItem = db.CartItem.Include(c => c.Cart);
            return View(cartItem.ToList());
        }

        // GET: CartItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItem/Create
        public ActionResult Create()
        {
            ViewBag.CartId = new SelectList(db.Cart, "Id", "Cookie");
            return View();
        }

        // POST: CartItem/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CartId,ProductId,Price,Quantity,CreatedOn,CreatedBy,ChangedOn,ChangedBy")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CartItem.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartId = new SelectList(db.Cart, "Id", "Cookie", cartItem.CartId);
            return View(cartItem);
        }

        // GET: CartItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartId = new SelectList(db.Cart, "Id", "Cookie", cartItem.CartId);
            return View(cartItem);
        }

        // POST: CartItem/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CartId,ProductId,Price,Quantity,CreatedOn,CreatedBy,ChangedOn,ChangedBy")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartId = new SelectList(db.Cart, "Id", "Cookie", cartItem.CartId);
            return View(cartItem);
        }

        // GET: CartItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItem.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: CartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItem.Find(id);
            db.CartItem.Remove(cartItem);
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
